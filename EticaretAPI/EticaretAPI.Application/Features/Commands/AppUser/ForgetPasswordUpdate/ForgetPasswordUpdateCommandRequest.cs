using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.ForgetPasswordUpdate
{
    public class ForgetPasswordUpdateCommandRequest :IRequest<ForgetPasswordUpdateCommandResponse>
    {
        public string UserId {  get; set; }
        public string ResetToken {  get; set; }
        public string newPassword { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
