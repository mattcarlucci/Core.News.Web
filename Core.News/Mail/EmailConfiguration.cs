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
namespace Core.News.Services
{
    
    /// <summary>
    /// Class EmailConfiguration.
    /// </summary>
    /// <seealso cref="Crypto.Compare.Configs.IEmailConfiguration" />
    public class EmailConfiguration : IEmailConfiguration
    {

        /// <summary>
        /// Gets or sets the SMTP.
        /// </summary>
        /// <value>The SMTP.</value>
        public HostConfiguration Smtp { get; set; }
        /// <summary>
        /// Gets or sets the pop.
        /// </summary>
        /// <value>The pop.</value>
        public HostConfiguration Pop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Crypto.Compare.Configs.IEmailConfiguration" /> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use SSL].
        /// </summary>
        /// <value><c>true</c> if [use SSL]; otherwise, <c>false</c>.</value>
        public bool UseSsl { get; set ; }

        /// <summary>
        /// Gets or sets the user configuration.
        /// </summary>
        /// <value>The user configuration.</value>
        public UserConfiguration User { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfiguration"/> class.
        /// </summary>
        public EmailConfiguration()
        {
            User = new UserConfiguration();
            Smtp = new HostConfiguration();
            Pop = new HostConfiguration();
        }
    }
}
