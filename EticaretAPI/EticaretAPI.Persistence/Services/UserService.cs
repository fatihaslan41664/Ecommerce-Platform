using Azure.Core;
using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Token.User;
using EticaretAPI.Application.DTOs.User;
using EticaretAPI.Application.Features.Commands.AppUser.CreateUser;
using EticaretAPI.Application.Helpers;
using EticaretAPI.Application.Repositories.EndPoint;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Identity;
using EticaretAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities.Identity;

namespace EticaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<T.AppUser> _userManager;
        readonly IEndpointReadRepository _endpointReadRepository;


        public UserService(UserManager<T.AppUser> userManager, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
        }

        public int TotalUserCount => _userManager.Users.Count();
        public async Task<CreateUserResponse> CreateAsync(CreateUser user)
        {
            
            IdentityResult identityResult = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                NameSurname = user.NameSurname,
                
            }, user.Password);
            CreateUserResponse response = new() { Succeeded = identityResult.Succeeded };
            if (identityResult.Succeeded)
            {
                response.Message = "Basarili sekilde olusturuldu";
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description} <br>";
                }
            }
            return response;
        }
        public async Task<List<ListUserDTO>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users.Skip(page*size).Take(size).ToListAsync();
            return users.Select(user => new ListUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.NameSurname,
                TwoFactorAuthEnabled = user.TwoFactorEnabled

            }).ToList();
        }
        public async Task UpdateForgetPassword(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null) {
                resetToken = resetToken.UrlDecode();
                IdentityResult result =await _userManager.ResetPasswordAsync(user,resetToken,newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new Exception("hata");
                }
        }
        public async Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDateTime, int refreshTokenLifeTime)
        {
            if(appUser != null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenEndDate = accessTokenDateTime.AddMinutes(refreshTokenLifeTime);
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                throw new Exception("User bulunamadi");
            }
        }
        public async Task AssignRoleUser(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null) 
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles != null) { 
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                }
                await _userManager.AddToRolesAsync(user, roles);
            }
        }
        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if(user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            if (user!=null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToArray();
            }
            else
            {
                throw new Exception("böyle bir kullanici yok");
            }
        }
        public async Task<bool> HasRoleForControllerAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any())
            {
                return false;
            }
            EndPoint? endpoint =await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);
            if (endpoint == null) { 
                return false;
            }
            var endpointRoles = endpoint.Roles.Select(r => r.Name);
            foreach (var userRole in userRoles)
            {
                 foreach (var endpointRole in endpointRoles)
                 {
                     if (userRole == endpointRole)
                     {
                         return true;

                     }
                 }
            }
            return false;
        }
    }
}
