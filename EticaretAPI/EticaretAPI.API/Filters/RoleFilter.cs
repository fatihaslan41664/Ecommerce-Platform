using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace EticaretAPI.API.Filters
{
    public class RoleFilter : IAsyncActionFilter
    {
        readonly IUserService _userService;

        public RoleFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity?.Name;

            if (!string.IsNullOrEmpty(name)&& name != "fth")
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var attribute = descriptor?.MethodInfo.GetCustomAttribute<AuthorizeDefinitionAttribute>();
                if (attribute != null)
                {
                    var httpAttribute = descriptor.MethodInfo.GetCustomAttribute<HttpMethodAttribute>();
                    var httpMethod = httpAttribute?.HttpMethods?.FirstOrDefault() ?? "GET";
                    var code = $"{httpMethod}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";

                    var hasRole = await _userService.HasRoleForControllerAsync(name, code);

                    if (!hasRole)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
            }
            else
                await next();
        }
    }
}
