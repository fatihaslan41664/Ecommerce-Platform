using EticaretAPI.Application.Repositories.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Application.Repositories.BasketItem
{
    public interface IBasketItemWriteRepostiyory : IWriteRepository<T.BasketItems>
    {
    }
}
