using EticaretAPI.Application.Repositories.OrderRep;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(EticaretAPIDbContext context) : base(context)
        {

        }

        public async Task<Order> GetOrderWithUserDetailsAsync(string id)
        {
            Order? order = await Table
                .Include(o => o.Basket)
                    .ThenInclude(b => b.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            if(order != null)
            {
                return order;
            }
            throw new Exception("order bulunamadı");
        }
    }
}
