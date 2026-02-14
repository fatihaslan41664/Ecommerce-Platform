using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Repositories.Basket;
using EticaretAPI.Application.Repositories.BasketItem;
using EticaretAPI.Application.Repositories.OrderRep;
using EticaretAPI.Application.ViewModels.Baskets;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpcontextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketItemWriteRepostiyory _basketItemWriteRepository;
        readonly IBasketItemReadRepostiyory _basketItemReadRepostiyory;
        readonly IBasketReadRepository _basketReadRepository;

        public BasketService(IHttpContextAccessor httpcontextAccessor,
            UserManager<AppUser> userManager,
            IOrderReadRepository orderReadRepository,
            IBasketWriteRepository basketWriteRepository,
            IBasketItemWriteRepostiyory basketItemWriteRepository,
            IBasketItemReadRepostiyory basketItemReadRepostiyory,
            IBasketReadRepository basketReadRepository)
        {
            _httpcontextAccessor = httpcontextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepostiyory = basketItemReadRepostiyory;
            _basketReadRepository = basketReadRepository;
        }
        private async Task<Basket> ContextUser()
        {
            // 1️⃣ Kullanıcı adını al (ClaimTypes.Name)
            var userName = _httpcontextAccessor?.HttpContext?.User
                                .FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
                throw new Exception("Beklenmeyen bir hata oluştu: kullanıcı bulunamadı.");

            // 2️⃣ Kullanıcıyı getir
            var user = await _userManager.Users
                .Include(u => u.Baskets)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            // 3️⃣ Aktif (Order ile ilişkisiz) sepeti bul
            var targetBasket = user.Baskets
                .FirstOrDefault(b => !_orderReadRepository.Table.Any(o => o.Id == b.Id));

            // 4️⃣ Eğer yoksa yeni Basket oluştur
            if (targetBasket == null)
            {
                targetBasket = new Basket();
                user.Baskets.Add(targetBasket);
                await _basketWriteRepository.SaveAsync();
            }

            // 5️⃣ Basket'i döndür
            return targetBasket;
        }

        public async Task AddItemToBasketAsync(VM_Create_BasketItem basketItem)
        {
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItems basket_Item= await _basketItemReadRepostiyory.GetSingleAsync(b => b.BasketId == basket.Id &&
                b.ProductId==Guid.Parse(basketItem.ProductId));
                if (basket_Item != null) 
                {
                    basket_Item.Quantity++;
                }
                else
                {
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity,
                    });
                }
                await _basketWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItems>> GetBasketItemsAsync()
        {
            Basket basket = await ContextUser();
            Basket result = await _basketReadRepository.Table.Include(b=> b.BasketItems)
                .ThenInclude(bi=>bi.Product)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);
            return result.BasketItems.ToList();
        }

        public async Task RemoveAsync(string basketItemId)
        {
            BasketItems item = await _basketItemReadRepostiyory.GetByIdAsync(basketItemId);
            if (item != null) { 
                _basketItemWriteRepository.Remove(item);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantity(VM_Update_BasketItem basketItem)
        {
            BasketItems item = await _basketItemReadRepostiyory.GetByIdAsync(basketItem.BasketItemId);
            if (item != null) { 
                item.Quantity = basketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }
        public async Task<Basket> GetUserActiveBasket()
        {
            Basket basket = await ContextUser();
            return basket;
        }
    }
}
