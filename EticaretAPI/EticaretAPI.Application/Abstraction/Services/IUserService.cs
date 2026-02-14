using EticaretAPI.Application.Abstraction.Token.User;
using EticaretAPI.Application.DTOs.User;
using EticaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser user);
        Task UpdateRefreshToken(string refreshToken, AppUser Appuser, DateTime accessTokenDateTime, int refreshTokenLifeTime);
        Task UpdateForgetPassword(string userId, string resetToken, string newPassword);
        Task<List<ListUserDTO>> GetAllUsersAsync(int page, int size);
        int TotalUserCount {  get; }
        Task AssignRoleUser(string userId, string[]roles);
        Task<string[]>GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRoleForControllerAsync(string name, string code);
    }
}
