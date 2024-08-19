using Microsoft.Extensions.Options;
using PsikoWeb.WebApp.OptionsModel;
using System.Net;
using System.Net.Mail;

namespace PsikoWeb.WebApp.Services
{
    public class EmailService:IEmailService
    {
        private readonly EmailSettings _emailsettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailsettings = options.Value;
        }

        async Task IEmailService.SendResetPasswordEmail(string resetPasswordEmailLink, string toEmail)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _emailsettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailsettings.Email,_emailsettings.Password);
            smtpClient.EnableSsl = true;
            var emailMessage = new MailMessage();

            emailMessage.From = new MailAddress(_emailsettings.Email);
            emailMessage.To.Add(toEmail);
            emailMessage.Subject = "Şifre sıfırlama linki";
            emailMessage.Body = @$"<h4>Şifrenizi yenilemek için aşağıdaki line tıklayınız</h4>
                                   <p>
                                    <a href ='{resetPasswordEmailLink}'>Şifre yenileme link</a>
                                   </p>";
            emailMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync(emailMessage);   
        }
    }
}
