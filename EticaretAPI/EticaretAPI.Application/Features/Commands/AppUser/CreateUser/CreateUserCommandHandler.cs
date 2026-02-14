using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Token.User;
using EticaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities.Identity;

namespace EticaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {

        readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                ConiformPassword = request.ConiformPassword,
                UserName = request.UserName,
            });
            if(request.Password == request.ConiformPassword)
            {
            return new() { Message = response.Message, Succeeded= response.Succeeded};
            }
            else
            {
                throw new Exception("Şifreler eşleşmiyor");
            }
        }
    }
}
