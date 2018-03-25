// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-18-2018
// ***********************************************************************
// <copyright file="EmailAddress.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Newtonsoft.Json;
using System;
using System.Net.Mail;

namespace Core.News.Mail
{


    /// <summary>
    /// Class EmailAddress.
    /// </summary>
    public class EmailAddress
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress" /> class.
        /// </summary>
        public EmailAddress()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="address">The address.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public EmailAddress(string name, string address, bool enabled, string schedule)
        {
            this.DisplayName = name;
            this.Address = address;
            this.Enabled = enabled;
            this.Schedule = schedule;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="address">The address.</param>
        /// <param name="schedule">The schedule.</param>
        public EmailAddress(string name, string address, string schedule)
        {
            this.DisplayName = name;
            this.Address = address;        
            this.Schedule = schedule;
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EmailAddress" /> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <value>The schedule.</value>
        public string Schedule { get; set; }

        /// <summary>
        /// Gets or sets the last sent.
        /// </summary>
        /// <value>The last sent.</value>
        public DateTime LastSent { get; set; }
        /// <summary>
        /// Mails the address.
        /// </summary>
        /// <returns>MailAddress.</returns>
        [JsonIgnore]
        public MailAddress MailAddress
            { get {   return new System.Net.Mail.MailAddress(Address, DisplayName); }}

        
    }
}
