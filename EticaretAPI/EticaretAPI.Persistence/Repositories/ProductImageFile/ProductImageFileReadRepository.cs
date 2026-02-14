using EticaretAPI.Application.Repositories;
using T = EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileReadRepository : ReadRepository<T.ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
