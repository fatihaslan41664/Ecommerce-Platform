using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Features.Queries.Order.GetAllOrders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.AssignRoleEndPoint.GetRolesEndPoints
{
    public class GetRolesEndPointQueryHandler : IRequestHandler<GetRolesEndPointQueryRequest, GetRolesEndPointQueryResponse>
    {
        readonly IAuthorizeEndPointService _authorizeEndPointService;

        public GetRolesEndPointQueryHandler(IAuthorizeEndPointService authorizeEndPointService)
        {
            _authorizeEndPointService = authorizeEndPointService;
        }

        public async Task<GetRolesEndPointQueryResponse> Handle(GetRolesEndPointQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _authorizeEndPointService.GetRolestoEndPointAsync(request.Code,request.Menu);
            return new()
            {
                Roles = datas
            };
        }
    }
}
