using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddBasketItemsCommandHandler : IRequestHandler<AddBasketItemsCommandRequest, AddBasketItemsCommandResponse>
    {
        readonly IBasketService _basketService;

        public AddBasketItemsCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<AddBasketItemsCommandResponse> Handle(AddBasketItemsCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.AddItemToBasketAsync(new()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            });
            return new();

        }
    }
}
