// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="IEmailConfiguration.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Core.News.Mail
{
    /// <summary>
    /// Interface IEmailConfiguration
    /// </summary>
    public interface IEmailConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IEmailConfiguration"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; set; }
      
        /// <summary>
        /// Gets or sets the SMTP.
        /// </summary>
        /// <value>The SMTP.</value>
        SmtpConfiguration Smtp { get; set; }

        /// <summary>
        /// Gets or sets the rest SMTP.
        /// </summary>
        /// <value>The rest SMTP.</value>
        RestSmtpConfiguration RestSmtp { get; set; }

        /// <summary>
        /// Gets or sets the SMTP client.
        /// </summary>
        /// <value>The SMTP client.</value>
        string SmtpClient { get; set; }
      
        /// <summary>
        /// Gets or sets the user configuration.
        /// </summary>
        /// <value>The user configuration.</value>
        UserConfiguration Users { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        EmailAddress From { get; set; }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns>EmailConfiguration.</returns>
        EmailConfiguration Load();

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <returns>List&lt;EmailAddress&gt;.</returns>
        List<EmailAddress> GetAddresses();
        /// <summary>
        /// Clones the specified schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns>EmailConfiguration.</returns>
        EmailConfiguration Clone(string schedule);
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>UserConfiguration.</returns>
        UserConfiguration GetUsers(Func<EmailAddress, bool> p);

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns>UserConfiguration.</returns>
        UserConfiguration GetUsers();
    }
}