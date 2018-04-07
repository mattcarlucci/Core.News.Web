// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="EmailConfiguration.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.News.Mail
{
    /// <summary>
    /// Class EmailConfiguration.
    /// </summary>
    /// <seealso cref="Crypto.Compare.Configs.IEmailConfiguration" />
    public class EmailConfiguration : IEmailConfiguration
    {
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// Gets or sets the SMTP.
        /// </summary>
        /// <value>The SMTP.</value>
        public SmtpConfiguration Smtp { get; set; }
       
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Core.News.Mail.IEmailConfiguration" /> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the user configuration.
        /// </summary>
        /// <value>The user configuration.</value>
        public UserConfiguration Users { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public EmailAddress From { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfiguration"/> class.
        /// </summary>
        private EmailConfiguration()
        {
            Users = new UserConfiguration();
            Smtp = new SmtpConfiguration();
            From = new EmailAddress();          
        }

        public EmailConfiguration(ILoggerFactory loggerFactory) : this()
        {
            this.loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <returns>List&lt;EmailAddress&gt;.</returns>
        public List<EmailAddress> GetAddresses()
        {
            List<EmailAddress> addresses = new List<EmailAddress>();
            addresses.AddRange(Users.To.Where(w => w.Enabled));
            addresses.AddRange(Users.Cc.Where(w => w.Enabled));
            addresses.AddRange(Users.Bcc.Where(w => w.Enabled));
            return addresses;
        }
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>UserConfiguration.</returns>
        public UserConfiguration GetUsers(Func<EmailAddress, bool> predicate)
        {
            UserConfiguration config = new UserConfiguration
            {
                To = Users.To.Where(predicate).ToList(),
                Cc = Users.Cc.Where(predicate).ToList(),
                Bcc = Users.Bcc.Where(predicate).ToList()
            };
            return config;
        }
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns>UserConfiguration.</returns>
        public UserConfiguration GetUsers()
        {
            UserConfiguration config = new UserConfiguration
            {
                To = Users.To,
                Cc = Users.Cc.ToList(),
                Bcc = Users.Bcc.ToList()
            };
            return config;
        }

        /// <summary>
        /// Clones the specified schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns>EmailConfiguration.</returns>
        public EmailConfiguration Clone(string schedule)
        {
            EmailConfiguration config = new EmailConfiguration
            {
                Smtp = this.Smtp,
                From = this.From,
                Users = GetUsers(w => w.Schedule == schedule)
            };
            return config;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns>EmailConfiguration.</returns>
        public EmailConfiguration Load()
        {
            return EmailConfigContext.Load(loggerFactory);
        }     
    }
}
