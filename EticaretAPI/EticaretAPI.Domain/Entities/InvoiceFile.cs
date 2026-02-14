using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class InvoiceFile : MyFile
    {
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Price { get; set; }
    }
}
