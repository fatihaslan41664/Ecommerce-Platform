using EticaretAPI.Application.ViewModels.Baskets;
using EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{

    public interface IBasketService
    {
        public Task<List<BasketItems>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem);
        public Task UpdateQuantity(VM_Update_BasketItem basketItem);
        public Task RemoveAsync(string basketItemId);
        public Task<Basket> GetUserActiveBasket();
    }
}
