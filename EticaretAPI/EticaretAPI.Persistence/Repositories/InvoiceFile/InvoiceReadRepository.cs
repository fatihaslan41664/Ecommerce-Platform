using T = EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Repositories.InvoiceFile;
using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.Persistence.Repositories.InvoiceFile
{
    public class InvoiceReadRepository : ReadRepository<T.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
