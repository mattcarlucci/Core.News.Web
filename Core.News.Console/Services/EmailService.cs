// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="EmailService.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Linq;
using static Core.News.Mail.EmailService;
using Microsoft.Extensions.Logging;

namespace Core.News.Mail
{
    /// <summary>
    /// Class EmailService.
    /// </summary>
    /// <seealso cref="Crypto.Compare.Configs.IEmailService" />
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        /// <summary>
        /// The email configuration
        /// </summary>
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly SmtpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="emailConfiguration">The email configuration.</param>
        public EmailService(IEmailConfiguration emailConfiguration, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmailService>();
            _emailConfiguration = emailConfiguration;

            client = new SmtpClient(_emailConfiguration.Smtp.Host, _emailConfiguration.Smtp.Port)
            {
                Credentials = new NetworkCredential(_emailConfiguration.Smtp.UserName, _emailConfiguration.Smtp.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = _emailConfiguration.UseSsl
            };
        }
        
        /// <summary>
        /// Sends the specified email message.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Send(string subject, string body)
        {
            var cfg = _emailConfiguration;

            if (cfg.Enabled == false || cfg.UserConfiguration.To.Count() == 0) return;

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(cfg.UserConfiguration.From.Address, cfg.UserConfiguration.From.Name),
                SubjectEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            };

            foreach (var user in cfg.UserConfiguration.To.Where(w => w.Enabled))
            {
                mail.To.Add(user.Address);
            }
            foreach (var user in cfg.UserConfiguration.Cc.Where(w => w.Enabled))
            {
                mail.CC.Add(user.Address);
            }
            foreach (var user in cfg.UserConfiguration.Bcc.Where(w => w.Enabled))
            {
                mail.Bcc.Add(user.Address);
            }

            _logger.LogInformation("Sending email...");
            client.Send(mail);
            _logger.LogInformation("Success");
        }
    }
}
