// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="Program.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News.Mail;
using Core.News.Services;
using Crypto.Compare;
using Crypto.Compare.Proxies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using News.Core.SqlServer;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.News
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {      
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Bootstrap.InitializeAsync();
        }
      
    }

}
