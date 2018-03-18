// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="EmailAddress.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Core.News.Mail
{
    /// <summary>
    /// Class EmailMessage.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class.
        /// </summary>
        public EmailMessage()
        {
            From = new EmailAddress();
            To = new List<EmailAddress>();
            Cc = new List<EmailAddress>();
            Bcc = new List<EmailAddress>();
           
        }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public EmailAddress From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public List<EmailAddress> To { get; set; }
        /// <summary>
        /// Gets the cc.
        /// </summary>
        /// <value>The cc.</value>
        public List<EmailAddress> Cc { get; }
        /// <summary>
        /// Gets the BCC.
        /// </summary>
        /// <value>The BCC.</value>
        public List<EmailAddress> Bcc { get; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; set; }
    }
}
