using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User {get;set;}
        public Order Order { get; set; }
        public ICollection<BasketItems> BasketItems { get; set; }

    }
}
