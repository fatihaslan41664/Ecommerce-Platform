using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Basket.UpdateQuantityBasket
{
    public class UpdateQuantityBasketRequest : IRequest<UpdateQuantityBasketResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
