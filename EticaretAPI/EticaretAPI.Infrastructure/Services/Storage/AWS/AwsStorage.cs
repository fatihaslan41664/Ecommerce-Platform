using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using EticaretAPI.Application.Abstraction.Storage.Aws;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EticaretAPI.Infrastructure.Services.Storage.AWS
{
    public class AWSStorage : IAWSStorage
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public string StorageName => "AWS";

        public AWSStorage(IConfiguration configuration)
        {
            var awsConfig = configuration.GetSection("AWS");
            var accessKey = awsConfig["AccessKey"];
            var secretKey = awsConfig["SecretKey"];
            var region = awsConfig["Region"];
            _bucketName = awsConfig["BucketName"];
            _s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
        }

        private async Task<string> FileRenameAsync(string pathOrContainer, string fileName)
        {
            string newFileName = fileName;
            int counter = 1;

            string extension = Path.GetExtension(fileName);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

            while (await HasFileInternalAsync(pathOrContainer, newFileName))
            {
                newFileName = $"{fileNameWithoutExt}_{counter}{extension}";
                counter++;
            }
            return newFileName;
        }

        private async Task<bool> HasFileInternalAsync(string pathOrContainer, string fileName)
        {
            try
            {
                string key = $"{pathOrContainer}/{fileName}";
                var request = new GetObjectMetadataRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                await _s3Client.GetObjectMetadataAsync(request);
                return true;
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainer, IFormFileCollection files)
        {
            var uploadedFiles = new List<(string fileName, string pathOrContainer)>();

            if (files == null || !files.Any())
                return uploadedFiles;

            try
            {
                var transferUtility = new TransferUtility(_s3Client);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        // Güvenli dosya adı oluştur
                        string originalFileName = Path.GetFileName(file.FileName);
                        string safeFileName = await FileRenameAsync(pathOrContainer, originalFileName);

                        // S3 key'i (tam path)
                        string key = $"{pathOrContainer}/{safeFileName}";

                        // Upload request oluştur
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = file.OpenReadStream(),
                            BucketName = _bucketName,
                            Key = key,
                            ContentType = file.ContentType,
                            ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
                        };

                        // Dosyayı S3'e yükle
                        await transferUtility.UploadAsync(uploadRequest);

                        // Başarılı yükleme sonrası listeye ekle - TAM PATH DÖNDÜR
                        uploadedFiles.Add((safeFileName, key));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"S3 upload failed: {ex.Message}", ex);
            }

            return uploadedFiles;
        }

        public async Task DeleteAsync(string pathOrContainer, string fileName)
        {
            try
            {
                string key = $"{pathOrContainer}/{fileName}";
                await _s3Client.DeleteObjectAsync(_bucketName, key);
            }
            catch (Exception ex)
            {
                throw new Exception($"Delete failed: {ex.Message}", ex);
            }
        }

        public List<string> GetFiles(string pathOrContainer)
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = _bucketName,
                    Prefix = $"{pathOrContainer}/",
                    MaxKeys = 1000
                };

                var response = _s3Client.ListObjectsV2Async(request).GetAwaiter().GetResult();
                return response.S3Objects.Select(x => x.Key).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"List files failed: {ex.Message}", ex);
            }
        }

        public bool Hasfile(string pathOrContainer, string fileName)
        {
            return HasFileInternalAsync(pathOrContainer, fileName).GetAwaiter().GetResult();
        }
    }
}