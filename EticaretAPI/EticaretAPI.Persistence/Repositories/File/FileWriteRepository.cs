using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.Repositories.File;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<MyFile>, IFileWriteRepository
    {
        public FileWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
