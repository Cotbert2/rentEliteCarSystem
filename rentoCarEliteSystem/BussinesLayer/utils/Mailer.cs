using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MailKit.Security;


namespace BussinesLayer.utils
{
    public class Mailer
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly bool _enableSSL;

        public Mailer()
        {

        }

        public async Task<bool> SendEmailAsync(string to, string subject, string htmlBody)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Rent a Car Elite | System", _senderEmail));
            email.To.Add(new MailboxAddress("", to));
            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = htmlBody
            };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_senderEmail, _senderPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }
}
