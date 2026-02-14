using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string toWho ,string subject,string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] toWhoes, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMail(string to, string userId, string resetToken);
        Task SendMailCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate,string userNamee);
    }
}
