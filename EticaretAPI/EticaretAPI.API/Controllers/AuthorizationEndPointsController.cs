using EticaretAPI.Application.Features.Commands.AutohrizeRoleEndpoint.AssignRoleEndPoint;
using EticaretAPI.Application.Features.Queries.AssignRoleEndPoint.GetRolesEndPoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    
    public class AuthorizationEndPointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndPointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleEndPointCommandRequest request)
        {
            request.Type = typeof(Program);
            AssignRoleEndPointCommandResponse response =  await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesEndPoint([FromBody] GetRolesEndPointQueryRequest request)
        {
            GetRolesEndPointQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
