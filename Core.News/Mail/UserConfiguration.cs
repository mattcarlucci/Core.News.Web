// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-18-2018
// ***********************************************************************
// <copyright file="UserConfiguration.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;

namespace Core.News.Services
{
    /// <summary>
    /// Class EmailUserConfiguration.
    /// </summary>
    public class UserConfiguration
    {
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
        /// Gets or sets the cc.
        /// </summary>
        /// <value>The cc.</value>
        public List<EmailAddress> Cc { get; set; }
        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        /// <value>The BCC.</value>
        public List<EmailAddress> Bcc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserConfiguration" /> class.
        /// </summary>
        public UserConfiguration()
        {
            From = new EmailAddress();
            To = new List<EmailAddress>();
            
            Cc = new List<EmailAddress>();
            Bcc = new List<EmailAddress>();
        }
    }
}
