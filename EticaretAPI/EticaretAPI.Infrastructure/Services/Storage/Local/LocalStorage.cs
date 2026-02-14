using EticaretAPI.Application.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        readonly IWebHostEnvironment webHostEnvironment;
        public string StorageName => "Local";

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
            => File.Delete($"{path}\\{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo dir = new(path);
            return dir.GetFiles().Select(f => f.Name).ToList();
        }

        public bool Hasfile(string path, string fileName)
            => File.Exists($"{path}\\{fileName}");

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                // Yeni dosya adını al
                string fileNewName = await FileRenameAsync(file.FileName, uploadPath);

                // Yeni dosya adıyla dosyayı kaydet
                string fullPath = Path.Combine(uploadPath, fileNewName);
                bool result = await CopyFileAsync(fullPath, file);

                // Yeni dosya adını listeye ekle
                datas.Add((fileNewName, Path.Combine(path, fileNewName)));
            }

            return datas;
        }

        async Task<string> FileRenameAsync(string fileName, string path)
        {
            string extension = Path.GetExtension(fileName);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = fileName;
            int counter = 1;

            // Aynı isimde dosya var mı kontrol et
            while (File.Exists(Path.Combine(path, newFileName)))
            {
                counter++;
                newFileName = $"{fileNameWithoutExt}-{counter}{extension}";
            }

            return newFileName;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream filestream = new(path, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(filestream);
                await filestream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}