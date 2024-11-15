﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemService.Interface
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string recipientEmail, string recipientName, DateTime validUntil);
    }
}
