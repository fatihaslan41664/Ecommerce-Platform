using T =EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Repositories.ProductImageFile;
using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<T.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
