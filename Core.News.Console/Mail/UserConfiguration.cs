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

namespace Core.News.Mail
{
    /// <summary>
    /// Class EmailUserConfiguration.
    /// </summary>
    public class UserConfiguration
    {
        
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
            To = new List<EmailAddress>();            
            Cc = new List<EmailAddress>();
            Bcc = new List<EmailAddress>();
        }
        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>List&lt;EmailAddress&gt;.</returns>
        public List<EmailAddress> All()
        {
            List<EmailAddress> all = new List<EmailAddress>();
            all.AddRange(To);
            all.AddRange(Cc);
            all.AddRange(Bcc);
            return all;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("To: {0} Cc: {1} Bcc: {2}", 
                string.Join(";", To.Select(s => s.Address)),
                string.Join(";", Cc.Select(s => s.Address)),
                string.Join(";", Bcc.Select(s => s.Address)));

        }
    }
}
