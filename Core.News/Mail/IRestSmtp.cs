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

namespace Core.News.Mail
{
    public interface IRestSmtp
    {       
         string ApiKey { get; set; }
      
         string Domain { get; set; }
      
         string HostUrl { get; set; }
    }
}