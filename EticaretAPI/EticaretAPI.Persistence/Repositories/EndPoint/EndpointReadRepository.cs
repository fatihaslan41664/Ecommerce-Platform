using T=EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Repositories.EndPoint;
using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.Persistence.Repositories.EndPoint
{
    public class EndpointReadRepository : ReadRepository<T.EndPoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
