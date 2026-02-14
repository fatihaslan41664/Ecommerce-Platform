using EticaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Series { get; set; }            
        public string Category { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }           
        public int Stock { get; set; }                
        public double Height { get; set; }                   
        public string Color { get; set; }             
        public string Description { get; set; }                
        //public ICollection<Order> Orders { get; set; }
        public ICollection<ProductImageFile> ProductImages { get; set; }
        public ICollection<BasketItems> BasketItems { get; set; }
    }
}
