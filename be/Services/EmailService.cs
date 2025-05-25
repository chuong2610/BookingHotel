using BookingHotel.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BookingHotel.Services{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration){
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string  to, string subject, string body)
        {
           var email = new MimeMessage(); 
           email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
           email.To.Add(MailboxAddress.Parse(to));
           email.Subject = subject;

           var builder = new BodyBuilder{
                 HtmlBody = body
           };
             
           email.Body = builder.ToMessageBody();
           var smtpClient = new SmtpClient();
           await smtpClient.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), SecureSocketOptions.StartTls);
           await smtpClient.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
           await smtpClient.SendAsync(email);
           await smtpClient.DisconnectAsync(true); 

        }
    }
}