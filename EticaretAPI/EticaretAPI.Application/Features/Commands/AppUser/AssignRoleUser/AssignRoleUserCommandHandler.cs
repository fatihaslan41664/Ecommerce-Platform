using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.AssignRoleUser
{
    public class AssignRoleUserCommandHandler : IRequestHandler<AssignRoleUserCommandRequest, AssignRoleUserCommandResponse>
    {
        readonly IUserService _userService;

        public AssignRoleUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AssignRoleUserCommandResponse> Handle(AssignRoleUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.AssignRoleUser(request.UserId,request.Roles);
            return new();
        }
    }
}
