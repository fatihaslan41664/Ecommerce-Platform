using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class ProductImageFile : MyFile
    {
        public bool showCase {  get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
