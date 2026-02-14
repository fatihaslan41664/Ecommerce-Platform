using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AutohrizeRoleEndpoint.AssignRoleEndPoint
{
    public class AssignRoleEndPointCommandRequest : IRequest<AssignRoleEndPointCommandResponse>
    {
        public string[] RoleIds { get; set; }
        public string EndPointCode { get; set; }
        public string Menu {  get; set; }
        public Type? Type { get; set; }
    }
}
