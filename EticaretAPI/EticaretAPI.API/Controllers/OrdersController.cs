using EticaretAPI.Application.Consts;
using EticaretAPI.Application.CustomAttributes;
using EticaretAPI.Application.Features.Commands.Order.CompleteOrder;
using EticaretAPI.Application.Features.Commands.Order.CreateOrder;
using EticaretAPI.Application.Features.Commands.Order.DeleteOrder;
using EticaretAPI.Application.Features.Commands.Product.DeleteProduct;
using EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile;
using EticaretAPI.Application.Features.Queries.Order.GetAllOrders;
using EticaretAPI.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Writing,
            Definition = "Create")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {

            CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Reading,
            Definition = "Get All Orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] int page = 0, [FromQuery] int size = 5)
        {
            var request = new GetAllOrdersQueryRequest
            {
                Page = page,
                Size = size
            };

            GetAllOrdersQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [HttpGet("{orderId}")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Reading,
            Definition = "Get Order By Id")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var response = await _mediator.Send(
                new GetOrderByIdQueryRequest { OrderId = orderId });

            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Deleting,
            Definition = "Delete Order")]
        public async Task<IActionResult> Delete([FromRoute] DeleteOrderCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Updating,
            Definition = "Complete Order")]
        public async Task<IActionResult> CompleteOrder([FromRoute]CompleteOrderCommandRequest request)
        {
            CompleteOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
