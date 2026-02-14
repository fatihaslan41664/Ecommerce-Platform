using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Basket.UpdateQuantityBasket
{
    public class UpdateQuantityBasketHandler : IRequestHandler<UpdateQuantityBasketRequest, UpdateQuantityBasketResponse>
    {
        readonly IBasketService _basketService;

        public UpdateQuantityBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<UpdateQuantityBasketResponse> Handle(UpdateQuantityBasketRequest request, CancellationToken cancellationToken)
        {
            await _basketService.UpdateQuantity(new()
            {
                Quantity = request.Quantity,
                BasketItemId = request.BasketItemId,
            });
            return new();

        }
    }
}
