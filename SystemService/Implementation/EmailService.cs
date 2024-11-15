
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendVerificationEmailAsync(string recipientEmail, string recipientName, DateTime validUntil)
        {
            string subject = "Your Free Ticket Has Been Verified!";
            string message = $@"
            Hello {recipientName},

            Congratulations! Your free ticket has been verified by our staff.

            Ticket Details:
            - Valid Until: {validUntil:yyyy-MM-dd} (expires in 30 days)

            How to use:
            - Show this Ticket to the driver when you're using one.

            Please make sure to use your ticket before it expires.

            Best regards,
            City Bus Management System";

            using (var mailMessage = new MailMessage("passswp@gmail.com", recipientEmail, subject, message))
            {
                try
                {
                    await _smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }
    }
}