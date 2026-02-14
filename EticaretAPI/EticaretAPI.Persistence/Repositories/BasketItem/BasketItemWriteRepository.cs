using EticaretAPI.Application.Repositories.BasketItem;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Persistence.Repositories.BasketItem
{
    public class BasketItemWriteRepository : WriteRepository<T.BasketItems>, IBasketItemWriteRepostiyory
    {
        public BasketItemWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
