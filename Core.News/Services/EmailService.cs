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

using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Linq;
using static Core.News.Services.EmailService;
using Microsoft.Extensions.Logging;
using Core.News.Mail;

namespace Core.News.Services
{
    /// <summary>
    /// Class EmailService.
    /// </summary>
    /// <seealso cref="Crypto.Compare.Configs.IEmailService" />
    public class EmailService : IEmailService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<EmailService> logger;
        /// <summary>
        /// The email configuration
        /// </summary>
        private readonly IEmailConfiguration settings;
        /// <summary>
        /// The client
        /// </summary>
        private readonly SmtpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="emailConfiguration">The email configuration.</param>
        public EmailService(IEmailConfiguration emailConfiguration, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<EmailService>();
            settings = emailConfiguration;

            client = new SmtpClient(settings.Smtp.Host, settings.Smtp.Port)
            {
                Credentials = new NetworkCredential(settings.Smtp.UserName, settings.Smtp.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = settings.UseSsl
               // UseDefaultCredentials = true
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
            if (settings.Enabled == false || settings.User.To.Count() == 0) return;

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(settings.User.From.Address, settings.User.From.Name),
                SubjectEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            };
           

            mailMessage.To.AddRange(settings.User.To.Where(w => w.Enabled));
            mailMessage.CC.AddRange(settings.User.Cc.Where(w => w.Enabled));
            mailMessage.Bcc.AddRange(settings.User.Bcc.Where(w => w.Enabled));
            

            logger.LogInformation("Sending email...");
            client.Send(mailMessage);
            logger.LogInformation("Success");
        }
    }
}
