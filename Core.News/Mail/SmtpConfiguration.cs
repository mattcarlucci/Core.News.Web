// ***********************************************************************
// Assembly         : 
// Author           : mcarlucci
// Created          : 03-24-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-24-2018
// ***********************************************************************
// <copyright file="SmtpConfiguration.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using System.Net;

namespace Core.News.Mail
{
    /// <summary>
    /// Class HostConfiguration.
    /// </summary>
    public class SmtpConfiguration
    {
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        [JsonIgnore]
        public NetworkCredential Credentials
        { get { return new NetworkCredential(UserName, Password); } }

        /// <summary>
        /// Gets or sets the use default credentials.
        /// </summary>
        /// <value>The use default credentials.</value>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable SSL].
        /// </summary>
        /// <value><c>true</c> if [enable SSL]; otherwise, <c>false</c>.</value>
        public bool EnableSsl { get; set; }
    }
}