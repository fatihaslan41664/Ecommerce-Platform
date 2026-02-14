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
    public class BasketItemReadRepository : ReadRepository<T.BasketItems>, IBasketItemReadRepostiyory
    {
        public BasketItemReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
