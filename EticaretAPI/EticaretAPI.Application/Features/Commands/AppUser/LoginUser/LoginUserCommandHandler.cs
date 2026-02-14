using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.DTOs;
using EticaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities.Identity;




namespace EticaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;
        public LoginUserCommandHandler(
            IAuthService authService
            )
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _authService.LoginAsync(request.Email, request.Password,10);
                return new LonginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            catch (NotFoundUserExceptions)
            {
                return new LonginUserErrorCommandResponse()
                {
                    Message = "Kullanıcı bulunamadı"
                };
            }
        }
    }
}
