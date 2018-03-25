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
                col.Add(new MailAddress(address.Address, address.DisplayName));
            }
        }
    }
}
