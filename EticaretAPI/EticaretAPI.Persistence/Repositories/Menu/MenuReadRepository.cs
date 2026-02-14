using T=EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Repositories.Menu;
using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.Persistence.Repositories.Menu
{
    public class MenuReadRepository : ReadRepository<T.Menu>, IMenuReadRepositiory
    {
        public MenuReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
