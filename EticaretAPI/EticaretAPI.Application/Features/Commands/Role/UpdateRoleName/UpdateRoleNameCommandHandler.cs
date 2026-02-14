using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Role.UpdateRoleName
{
    public class UpdateRoleNameCommandHandler : IRequestHandler<UpdateRoleNameCommandRequest, UpdateRoleNameCommandResponse>
    {
        IRoleService _roleService;

        public UpdateRoleNameCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<UpdateRoleNameCommandResponse> Handle(UpdateRoleNameCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.UpdateRoleName(request.Id, request.Name);
            return new() { Succeeded = result };
        }
    }
}
