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
namespace Core.News.Services
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
        /// Gets or sets the pop.
        /// </summary>
        /// <value>The pop.</value>
        HostConfiguration Pop { get; set; }
        /// <summary>
        /// Gets or sets the SMTP.
        /// </summary>
        /// <value>The SMTP.</value>
        HostConfiguration Smtp { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [use SSL].
        /// </summary>
        /// <value><c>true</c> if [use SSL]; otherwise, <c>false</c>.</value>
        bool UseSsl { get; set; }

        /// <summary>
        /// Gets or sets the user configuration.
        /// </summary>
        /// <value>The user configuration.</value>
        UserConfiguration User { get; set; }
    }
}