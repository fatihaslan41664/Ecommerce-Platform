using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.CustomAttributes;
using EticaretAPI.Application.Enums;
using EticaretAPI.Application.Features.Commands.AppUser.AssignRoleUser;
using EticaretAPI.Application.Features.Commands.AppUser.CreateUser;
using EticaretAPI.Application.Features.Commands.AppUser.ForgetPasswordUpdate;
using EticaretAPI.Application.Features.Commands.AppUser.GoogleLogin;
using EticaretAPI.Application.Features.Commands.AppUser.LoginUser;
using EticaretAPI.Application.Features.Queries.AppUser.GetAllUser;
using EticaretAPI.Application.Features.Queries.AppUser.GetRolesToUser;
using EticaretAPI.Application.Features.Queries.AssignRoleEndPoint.GetRolesEndPoints;
using EticaretAPI.Application.Features.Queries.Order.GetAllOrders;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService _mailService;

        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> SifreyiSegistir()
        {
            return Ok();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]

        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 0, [FromQuery] int size = 5)
        {
            var request = new GetAllUserQueryRequest
            {
                Page = page,
                Size = size
            };
            GetAllUserQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete User Role", Menu = "Users")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            return Ok();
        }
        [HttpPost("assign-role-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Change or Command Role for one user", Menu = "Users")]
        public async Task<IActionResult> AssignRoleUser(AssignRoleUserCommandRequest request)
        {
            AssignRoleUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "GetAllRoles For Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute]GetRolesToUserQueryRequest request)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
//public async Task<IActionResult> GetAllOrders([FromQuery] int page = 0, [FromQuery] int size = 5)
//{
//    var request = new GetAllOrdersQueryRequest
//    {
//        Page = page,
//        Size = size
//    };

//    GetAllOrdersQueryResponse response = await _mediator.Send(request);
//    return Ok(response);
//}