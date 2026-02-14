using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AutohrizeRoleEndpoint.AssignRoleEndPoint
{
    public class AssignRoleEndPointCommandHandler : IRequestHandler<AssignRoleEndPointCommandRequest, AssignRoleEndPointCommandResponse>
    {
        readonly IAuthorizeEndPointService _authorizeEndPointService;

        public AssignRoleEndPointCommandHandler(IAuthorizeEndPointService authorizeEndPointService)
        {
            _authorizeEndPointService = authorizeEndPointService;
        }

        public async Task<AssignRoleEndPointCommandResponse> Handle(AssignRoleEndPointCommandRequest request, CancellationToken cancellationToken)
        {
            await _authorizeEndPointService.AssignRoleEndPointsAsync(request.RoleIds, request.Menu, request.EndPointCode, request.Type);
            return new();
        }
    }
}
