using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Role.UpdateRoleName
{
    public class UpdateRoleNameCommandRequest : IRequest<UpdateRoleNameCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
