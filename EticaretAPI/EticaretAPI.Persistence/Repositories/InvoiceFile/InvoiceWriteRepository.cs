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
    public class InvoiceWriteRepository : WriteRepository<T.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
