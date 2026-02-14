using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.Repositories.CompletedOrderRep;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Persistence.Repositories.CompletedOrder
{
    public class CompletedOrderWriteRepository : WriteRepository<T.CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
