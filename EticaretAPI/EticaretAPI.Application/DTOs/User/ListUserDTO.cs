using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.DTOs.User
{
    public class ListUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public bool TwoFactorAuthEnabled { get; set; }
    }
}
//Id = user.Id.ToString(),
//                Email = user.Email,
//                NameSurname = user.NameSurname,
//                TwofactorAuthEnabled = user.TwoFactorEnabled,