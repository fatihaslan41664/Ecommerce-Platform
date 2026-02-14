using EticaretAPI.Application.Features.Commands.Basket.AddItemToBasket;
using EticaretAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using EticaretAPI.Application.Features.Commands.Basket.UpdateQuantityBasket;
using EticaretAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EticaretAPI.Application.CustomAttributes;
using EticaretAPI.Application.Consts;
namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Basket, ActionType = Application.Enums.ActionType.Reading,
            Definition ="Get All Basket Items")]
        public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketItemsQueryRequest request) 
        {
            List<GetBasketItemsQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Basket, ActionType = Application.Enums.ActionType.Writing,
            Definition = "Add Item to Basket")]
        public async Task<IActionResult> AddItemToBasket(AddBasketItemsCommandRequest request)
        {
            AddBasketItemsCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{BasketItemId}")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Basket, ActionType = Application.Enums.ActionType.Deleting,
            Definition = "Remove Basket Items")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketItemCommandRequest request)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Basket, ActionType = Application.Enums.ActionType.Updating,
            Definition = "Update Basket Items")]
        public async Task<IActionResult> UpdatteBasketItem(UpdateQuantityBasketRequest request)
        {
            UpdateQuantityBasketResponse response = await _mediator.Send(request);
            return Ok(response);

        }
    }
}
