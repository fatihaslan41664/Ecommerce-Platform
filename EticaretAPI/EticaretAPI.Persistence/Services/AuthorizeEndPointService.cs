using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Services.Configurations;
using EticaretAPI.Application.Repositories.EndPoint;
using EticaretAPI.Application.Repositories.Menu;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class AuthorizeEndPointService : IAuthorizeEndPointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointWriteRepository;
        readonly IMenuReadRepositiory _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole> _roleManager;
        public AuthorizeEndPointService(IApplicationService applicationService,
            IEndpointReadRepository endpointReadRepository,
            IEndpointWriteRepository endpointWriteRepository,
            IMenuReadRepositiory menuReadRepository,
            IMenuWriteRepository menuWriteRepository,
            RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndPointsAsync(string[] roleIds, string menu, string code, Type type)
        {
            Menu? _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };
                await _menuWriteRepository.AddAsync(_menu);
                await _menuWriteRepository.SaveAsync();
            }

            EndPoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Menu)
                .Include(e => e.Roles) // Rolleri de include ediyoruz
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndPoint(type)
                    .FirstOrDefault(m => m.MenuName == menu)?
                    .Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definiton = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = _menu,
                };
                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }
            foreach(var role in endpoint.Roles)
                endpoint.Roles.Remove(role);

            foreach (var roleId in roleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    endpoint.Roles.Add(role);
                }
            }

            await _endpointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolestoEndPointAsync(string code, string menu)
        {
            EndPoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .Include(e => e.Menu)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            // Null kontrolü ekle!
            if (endpoint == null)
            {
                return new List<string>(); // Boş liste döndür
            }

            return endpoint.Roles.Select(r => r.Name).ToList();
        }
    }
}
