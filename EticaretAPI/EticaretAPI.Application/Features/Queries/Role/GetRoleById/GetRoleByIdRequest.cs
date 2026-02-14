using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdRequest :IRequest<GetRoleByIdResponse>
    {
        public string Id { get; set; }
    }
}
