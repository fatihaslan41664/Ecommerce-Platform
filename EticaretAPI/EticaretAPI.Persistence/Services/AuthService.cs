using Azure.Core;
using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Services.Authentication;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.DTOs;
using EticaretAPI.Application.Exceptions;
using EticaretAPI.Application.Features.Commands.AppUser.LoginUser;
using EticaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using T = EticaretAPI.Domain.Entities.Identity;

namespace EticaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenHandler _tokenHandler;
        readonly UserManager<T.AppUser> _manager;
        readonly IConfiguration _config;
        readonly SignInManager<T.AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IMailService _mailService;

        public AuthService(ITokenHandler tokenHandler, UserManager<T.AppUser> manager, IConfiguration config, SignInManager<T.AppUser> signInManager, IUserService userService, IMailService mailService = null)
        {
            _tokenHandler = tokenHandler;
            _manager = manager;
            _config = config;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<Token> GoogleLoginAsync(string idToken,int minute)
        {

            {
                try
                {
                    var settings = new GoogleJsonWebSignature.ValidationSettings()
                    {
                        Audience = new List<string> { _config["Google:GoogleID"]}
                    };
                    var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                    var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
                    T.AppUser user = await _manager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                    bool result = user != null;

                    if (user == null)
                    {
                        user = await _manager.FindByEmailAsync(payload.Email);

                        if (user == null)
                        {
                            user = new T.AppUser()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Email = payload.Email,
                                UserName = payload.Email,
                                NameSurname = $"{payload.GivenName} {payload.FamilyName}",
                                EmailConfirmed = true
                            };

                            var identityResult = await _manager.CreateAsync(user);
                            result = identityResult.Succeeded;
                            if (!identityResult.Succeeded)
                            {
                                var errors = string.Join(", ", identityResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
                                throw new Exception($"Kullanıcı oluşturulamadı: {errors}");
                            }
                        }
                    }

                    if (result)
                    {
                        var existingLogins = await _manager.GetLoginsAsync(user);
                        if (!existingLogins.Any(x => x.LoginProvider == info.LoginProvider && x.ProviderKey == info.ProviderKey))
                        {
                            await _manager.AddLoginAsync(user, info);
                        }
                    }
                    else
                    {
                        throw new Exception("Kullanıcı doğrulaması başarısız oldu");
                    }
                    Token token = _tokenHandler.CreateAccessToken(minute,user);
                    await _userService.UpdateRefreshToken(token.RefreshToken,user,token.Expiration,30);
                    return token;
                }
                catch (InvalidJwtException)
                {
                    throw new Exception("Google token doğrulaması başarısız oldu");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Google login hatası: {ex.Message}", ex);
                }
            }
        }

        public async Task<Token> LoginAsync(string email, string password, int minute)
        {
            T.AppUser user = await _manager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundUserExceptions();
            }

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (signInResult.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(minute, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 30);
                return token;
            }
            else
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            T.AppUser? user = await _manager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if(user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15,user);
                await _userService.UpdateRefreshToken(token.RefreshToken,user,token.Expiration,15);
                return token;
            }
            else
            {
                throw new Exception("Uyeliğinizin süresi sona erdi");
            }
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser user = await _manager.FindByEmailAsync(email);
            if (user == null)
                return;

            string resetToken = await _manager.GeneratePasswordResetTokenAsync(user);

            string encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(resetToken)
            );

            await _mailService.SendPasswordResetMail(
                email,
                user.Id,
                encodedToken
            );
        }

        public async Task<bool> VerifyResetToken(string resetToken, string userID)
        {
            AppUser user = await _manager.FindByIdAsync(userID);
            if (user != null)
            {
                string decodedToken = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(resetToken)
                );
                return await _manager.VerifyUserTokenAsync(
                    user,
                    _manager.Options.Tokens.PasswordResetTokenProvider,
                    "ResetPassword",
                    decodedToken
                );
            }
            return false;
        }
    }
}
