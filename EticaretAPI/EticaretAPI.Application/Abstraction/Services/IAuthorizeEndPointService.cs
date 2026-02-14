using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IAuthorizeEndPointService
    {
        Task AssignRoleEndPointsAsync(string[] roleIds, string menu, string code, Type type);
        Task<List<string>> GetRolestoEndPointAsync(string code, string menu);
    }
}
