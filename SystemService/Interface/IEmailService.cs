using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemService.Implementation;

namespace SystemService.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO request);
        Task SendVerificationEmailAsync(string recipientEmail, string recipientName, DateTime validUntil);
    }
}
