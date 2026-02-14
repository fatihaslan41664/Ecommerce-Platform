using EticaretAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder order);
        Task<List<ListOrder>> GetAllOrdersAsync();
        Task<SingleOrder> GetOrderByIdAsync(string id);
        Task<bool> CompleteOrder(string id);
    }
}
