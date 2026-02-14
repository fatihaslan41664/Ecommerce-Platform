using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.Order;
using EticaretAPI.Application.Repositories.CompletedOrderRep;
using EticaretAPI.Application.Repositories.OrderRep;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EticaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrder order)
        {
            var rnd = new Random();
            double val = rnd.NextDouble();          // 0.0 ile 1.0 arası
            long fractional13 = (long)(val * 1_000_000_000_0000);  // 13 basamak
            string code13 = fractional13.ToString("D13");
            await _orderWriteRepository.AddAsync(new()
            {

                Address = order.Address,
                Id = Guid.Parse(order.BasketId),
                Description = order.Description,
                OrderCode = code13
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<List<ListOrder>> GetAllOrdersAsync()
        {
            return await _orderReadRepository.Table.Include(o => o.Basket)
                 .ThenInclude(b => b.User)
                 .Include(o => o.Basket)
                     .ThenInclude(b => b.BasketItems)
                     .ThenInclude(bi => bi.Product)
                 .Select(o => new ListOrder
                 {
                     Id = o.Id,
                     CreatedDate = o.CreatedDate,
                     OrderCode = o.OrderCode,
                     TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                     UserName = o.Basket.User.UserName,
                     CompletedDateTime = o.CompletedDate,
                     IsCompleted = o.IsCompleted
                 })
                 .ToListAsync();
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                 .Include(o => o.Basket)
                 .ThenInclude(b => b.BasketItems)
                 .ThenInclude(bi => bi.Product)
                 .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            return new()
            {
                Id = data.Id.ToString(),
                BasketItems = data.Basket.BasketItems.Select(bi=> new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data.Address,
                CreatedDate = data.CreatedDate,
                Description = data.Description,
                OrderCode = data.OrderCode,
                CompletedDateTime = data.CompletedDate,
                IsCompleted = data.IsCompleted

            };
        }
            public async Task<bool> CompleteOrder(string id)
            {
                Order order = await _orderReadRepository.GetByIdAsync(id);
                if(order != null)
                {
                    await _completedOrderWriteRepository.AddAsync(new CompletedOrder
                    {
                        OrderId = Guid.Parse(id)
                    });
                    order.IsCompleted = true;
                    order.CompletedDate = DateTime.Now;
                    return await _completedOrderWriteRepository.SaveAsync() >0;
            }
                else
                {
                    throw new Exception("ilgili kullanici bulunamadi");
                    
                }
            }
    }
}
