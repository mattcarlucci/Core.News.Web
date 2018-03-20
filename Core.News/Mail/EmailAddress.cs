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

using System.Collections.Generic;
using System.Net.Mail;

namespace Core.News.Mail
{

    /// <summary>
    /// Class Extensions.
    /// </summary>
    public static class MailExtensions
    {
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="addresses">The addresses.</param>
        public static void AddRange(this MailAddressCollection col, IEnumerable<EmailAddress> addresses )
        {
            foreach(var address in addresses)
            {
                col.Add(new MailAddress(address.Address, address.Name));
            }
        }
    }


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
        public EmailAddress(string name, string address, bool enabled)
        {
            this.Name = name;
            this.Address = address;
            this.Enabled = enabled;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
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
    }
}
