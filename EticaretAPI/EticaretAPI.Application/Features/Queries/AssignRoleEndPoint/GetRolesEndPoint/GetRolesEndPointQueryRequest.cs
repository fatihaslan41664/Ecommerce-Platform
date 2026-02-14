using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.AssignRoleEndPoint.GetRolesEndPoints
{
    public class GetRolesEndPointQueryRequest :IRequest<GetRolesEndPointQueryResponse>
    {
        public string Code { get; set; }
        public string Menu { get; set; }
    }
}
