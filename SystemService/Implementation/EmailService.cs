
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit.Text;
using MimeKit;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["EmailUserName"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Body
            };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_configuration["EmailHost"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["EmailUserName"], _configuration["EmailPassword"]);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your needs
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        public async Task SendVerificationEmailAsync(string recipientEmail, string recipientName, DateTime validUntil)
        {
            var emailDto = new EmailDTO
            {
                To = recipientEmail,
                Subject = "Your Free Ticket Has Been Verified!",
                Body = $"Hello {recipientName},\r\n\r\n        Congratulations! Your free ticket has been verified by our staff.\r\n\r\n        Ticket Details:\r\n        - Valid Until: {validUntil:yyyy-MM-dd} (expires in 30 days)\r\n\r\n        How to use:\r\n        - Show this Ticket to the driver when you're using one.\r\n\r\n        Please make sure to use your ticket before it expires.\r\n\r\n        Best regards,\r\n        City Bus Management System"
            };
            await SendEmailAsync(emailDto);

        }
    }

    public class EmailDTO
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}