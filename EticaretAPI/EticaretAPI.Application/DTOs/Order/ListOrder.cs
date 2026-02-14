using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EticaretAPI.Application.DTOs.Order
{
    public class ListOrder
    {
        public Guid Id { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCompleted   { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
