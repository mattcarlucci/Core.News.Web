// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="IEmailService.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Core.News.Services
{
    /// <summary>
    /// Interface IEmailService
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends the specified email message.
        /// </summary>
        /// <param name="emailMessage">The email message.</param>
        void Send(string subject, string body);
        
        //List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
