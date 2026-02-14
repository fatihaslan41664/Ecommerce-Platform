using EticaretAPI.Application.CustomAttributes;
using EticaretAPI.Application.Enums;
using EticaretAPI.Application.Features.Commands.Role.CreateRole;
using EticaretAPI.Application.Features.Commands.Role.DeleteRole;
using EticaretAPI.Application.Features.Commands.Role.UpdateRoleName;
using EticaretAPI.Application.Features.Queries.Role.GetAllRole;
using EticaretAPI.Application.Features.Queries.Role.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {
        readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType =ActionType.Reading, Definition =" Get roles", Menu ="Roles")]
        public async Task<IActionResult> GetAllRole([FromQuery]GetAllRoleQueryRequest request)
        {
            GetAllRoleQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = " Get Role By Id", Menu = "Roles")]
        public async Task<IActionResult> GetRoleById([FromRoute]GetRoleByIdRequest request) {
            GetRoleByIdResponse response  =await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = "Roles")]
        public async Task<IActionResult> CreateRole(CreateRoleCommandRequest request)
        {
            CreateRoleCommandResponse response =  await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = "Roles")]
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute]UpdateRoleNameCommandRequest request) 
        {
            UpdateRoleNameCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = "Roles")]
        public async Task<IActionResult> DeleteRole([FromRoute]DeleteRoleCommandRequest request) {
            DeleteRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
