using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.Role.GetAllRole
{
    public class GetAllRoleQueryResponse
    {
        public IDictionary<string,string> Datas { get; set; }
        public int TotalRoleCount { get; set; }
    }
}
