using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Application.Repositories.InvoiceFile
{
    public interface IInvoiceFileWriteRepository : IWriteRepository<T.InvoiceFile>
    {

    }
}
