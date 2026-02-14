using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _config;

        public TokenHandler(IConfiguration config)
        {
            _config = config;
        }

        public Application.DTOs.Token CreateAccessToken(int minute, AppUser appUser)
        {
            Application.DTOs.Token token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_config["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience : _config["Token:Audience"],
                issuer : _config["Token:Issuer"],
                expires : token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials : signingCredentials,
                claims : new List<Claim>
                {
                    new(ClaimTypes.Name, appUser.UserName)
                }
                );
            //oylesine degisecek
            JwtSecurityTokenHandler tokenHand = new();
            token.AccessToken = tokenHand.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator numberGenerator = RandomNumberGenerator.Create();
            numberGenerator.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
