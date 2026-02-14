using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Abstraction.Services.Configurations;
using EticaretAPI.Application.CustomAttributes;
using EticaretAPI.Application.DTOs.Configuration;
using EticaretAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
namespace EticaretAPI.Infrastructure.Services.Configurations
{
    public class ApllicationService : IApplicationService
    {
        public List<MenuDTO> GetAuthorizeDefinitionEndPoint(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t));
            List<MenuDTO> menus = new List<MenuDTO>();
            if (controllers != null)
            {
            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                if (actions != null) 
                {
                    foreach(var action in actions)
                    {
                       var attributes = action.GetCustomAttributes(true);
                            if (attributes != null) 
                            {
                                MenuDTO menu = null;
                                var reference = attributes.FirstOrDefault(a=> a.GetType() == typeof(AuthorizeDefinitionAttribute)) as
                                    AuthorizeDefinitionAttribute;

                                if (!menus.Any(m => m.MenuName == reference.Menu))
                                {
                                    menu = new() { MenuName = reference.Menu };
                                    menus.Add(menu);
                                }
                                else
                                {
                                    menu = menus.FirstOrDefault(m => m.MenuName == reference.Menu);
                                }
                                ActionDTO _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType),reference.ActionType),
                                    Definition = reference.Definition
                                };
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsSubclassOf(typeof(HttpMethodAttribute)))
                                    as HttpMethodAttribute;
                                if (httpAttribute != null)
                                {
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                }
                                else
                                {
                                    _action.HttpType = HttpMethods.Get;
                                }
                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ","")}";
                                menu.Actions.Add(_action);
                            }
                    }
                }
            }

            }

            return menus;
        }
    }
}
