using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EticaretAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryResponse
    {
        public int TotalCount { get; set; }
        public object Orders { get; set; }
    }

    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDateTime { get; set; }  // ✅ Ekle
        public bool IsCompleted { get; set; }
    }
}
//orderCode: string
//userName:string
//totalPrice : number
//createdDate: Date;