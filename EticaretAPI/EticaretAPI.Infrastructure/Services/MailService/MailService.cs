using EticaretAPI.Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.MailService
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string toWho, string subject,string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { toWho }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] toWhos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage
            {
                IsBodyHtml = isBodyHtml,
                Subject = subject,
                Body = body,
                From = new MailAddress(_configuration["Mail:Username"])
            };

            foreach (var item in toWhos)
                mail.To.Add(item);

            using SmtpClient smtpClient = new SmtpClient
            {
                Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]),
                Port = int.Parse(_configuration["Mail:Port"] ?? "587"),
                EnableSsl = true,
                Host = _configuration["Mail:Host"]
            };

            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMail(string to, string userId, string resetToken)
        {
            string clientUrl = _configuration["AngularClientUrl"];

            string resetUrl = $"{clientUrl}/sifreyiguncelle/{userId}/{resetToken}";

            string body = $@"
                Merhaba,<br><br>
                Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><br>
                <strong>
                    <a href=""{resetUrl}"" target=""_blank"">
                        Yeni şifre talebi için tıklayınız
                    </a>
                </strong>
                <br><br>
                <span style=""color:gray;font-size:12px;"">
                    Not: Eğer bu talep tarafınızca gerçekleştirilmediyse lütfen bu maili dikkate almayınız.
                </span>";

            await SendMailAsync(to, "Şifre yenileme talebi", body);
        }

        public async Task SendMailCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string clientUrl = _configuration["AngularClientUrl"];
            string body = $@"
                Merhaba sayın {userName},<br><br>
                {orderDate} tarihinde vermiş olduğunuz {orderCode}numaralı siparişiniz kargoya verilmiştir<br><br>
                ";

            await SendMailAsync(to, "Siparişiniz Kargoya Verildi", body);
        }
    }
}
