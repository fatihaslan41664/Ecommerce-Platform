using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.AssignRoleUser
{
    public class AssignRoleUserCommandRequest :IRequest<AssignRoleUserCommandResponse>
    {
        public string UserId { get; set; }
        public string[]Roles { get; set; }
    }
}
