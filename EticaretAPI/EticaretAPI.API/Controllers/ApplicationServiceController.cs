using EticaretAPI.Application.Abstraction.Services.Configurations;
using EticaretAPI.Application.Consts;
using EticaretAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServiceController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition ="Get Authorize Definitions Endpoints",
            Menu = AuthorizeDefinitionConstants.ApplicationService)]
        public IActionResult GetAuthorizeDefinitionEndPoints() {
            var datas = _applicationService.GetAuthorizeDefinitionEndPoint(typeof(Program));
            return Ok(datas);
        }
    }
}
