using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.ForgetPasswordUpdate
{
    public class ForgetPasswordUpdateCommandHandler : IRequestHandler<ForgetPasswordUpdateCommandRequest, ForgetPasswordUpdateCommandResponse>
    {
        public readonly IUserService _userService;

        public ForgetPasswordUpdateCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ForgetPasswordUpdateCommandResponse> Handle(ForgetPasswordUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.newPassword.Equals(request.PasswordConfirm))
            {
                throw new Exception("lütfen şifreleri aynı giriniz");
            }
            await _userService.UpdateForgetPassword(request.UserId,request.ResetToken,request.newPassword);
            return new();
        }
    }
}
