using EticaretAPI.Application.Repositories.CompletedOrderRep;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Persistence.Repositories.CompletedOrder
{
    public class CompletedOrderReadRepository : ReadRepository<T.CompletedOrder>, ICompletedOrderReadRepository
    {
        public CompletedOrderReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
