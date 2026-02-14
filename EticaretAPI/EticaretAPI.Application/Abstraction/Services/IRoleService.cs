using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IRoleService
    {
        Task<bool> CreateRole(string name);
        Task<bool> DeleteRole(string id);
        Task<bool> UpdateRoleName(string id,string name);
        (IDictionary<string, string> roles, int totalCount) GetAllRole(int page, int size);
        Task<(string Id, string Role)> GetRoleById(string id);
    }
}
