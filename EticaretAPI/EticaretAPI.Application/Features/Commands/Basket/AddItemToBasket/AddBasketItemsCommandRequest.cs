using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Basket.AddItemToBasket
{

    public class AddBasketItemsCommandRequest : IRequest<AddBasketItemsCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
