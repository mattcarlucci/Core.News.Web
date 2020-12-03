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

using System.Net;

namespace Core.News.Mail
{
    public interface ISmtpConfiguration 
    {
        NetworkCredential Credentials { get; }
        bool EnableSsl { get; set; }
        string Host { get; set; }
        string Password { get; set; }
        int Port { get; set; }
        bool UseDefaultCredentials { get; set; }
        string UserName { get; set; }
    }
}