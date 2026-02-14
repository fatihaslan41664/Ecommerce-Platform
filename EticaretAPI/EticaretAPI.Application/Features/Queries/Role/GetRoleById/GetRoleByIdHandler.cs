using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, GetRoleByIdResponse>
    {
        readonly IRoleService roleService;

        public GetRoleByIdHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<GetRoleByIdResponse> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await roleService.GetRoleById(request.Id);
            return new() { Id = result.Id, Name = result.Role };
        }
    }
}
