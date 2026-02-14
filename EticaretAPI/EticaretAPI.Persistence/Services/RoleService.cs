using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            });
            if (result.Succeeded) { 
                return true;
            }
            throw new Exception("boyle bir rol daha once olusturulmus");
        }

        public async Task<bool> DeleteRole(string id)
        {
            IdentityResult result = await _roleManager.DeleteAsync(new() { Id = id });
            return result.Succeeded;
        }


        public async Task<bool> UpdateRoleName(string id,string name)
        {
            IdentityResult result = await _roleManager.UpdateAsync(new()
            {
                Id = id,
                Name = name
            });
            return result.Succeeded;
        }
        public (IDictionary<string, string> roles, int totalCount) GetAllRole(int page, int size)
        
        {
            var query = _roleManager.Roles.AsQueryable();
            var totalCount = query.Count();

            if (page == -1 && size == -1)
            {
                var allRoles = query.ToDictionary(
                    role => role.Id,
                    role => role.Name
                );

                return (allRoles, totalCount);
            }

            var roles = query
                .Skip((page) * size)
                .Take(size)
                .ToDictionary(
                    role => role.Id,
                    role => role.Name
                );

            return (roles, totalCount);
        }

        public async Task<(string Id, string Role)> GetRoleById(string id)
        {
            string role = await _roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }
    }
}
