using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<DTOs.Token> LoginAsync(string email, string password, int minute);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
