
using EticaretAPI.Application.Abstraction.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Storage
{
    public class StorageServices : IStorageServices
    {
        readonly IStorage storage;
        public StorageServices(IStorage storage)
        {
            this.storage = storage;
        }

        public string StorageName { get => storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainer, string fileName)
        {
            await storage.DeleteAsync(pathOrContainer, fileName);
        }

        public  List<string> GetFiles(string pathOrContainer)
        {
             return storage.GetFiles(pathOrContainer);
        }

        public bool Hasfile(string pathOrContainer, string fileName)
            =>storage.Hasfile(pathOrContainer, fileName);

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainer, IFormFileCollection file)
            => await storage.UploadAsync(pathOrContainer, file);
    }
}
