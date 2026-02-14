using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.Repositories.Basket;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Persistence.Repositories.Basket
{
    public class BasketWriteReposityory : WriteRepository<T.Basket>, IBasketWriteRepository
    {
        public BasketWriteReposityory(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
