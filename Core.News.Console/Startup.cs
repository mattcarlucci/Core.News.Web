// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="Startup.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News.Configs;
using Core.News.Mail;
using Crypto.Compare.Proxies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using News.Core.SqlServer;
using News.Core.SqlServer.Models;
using System.Security.Cryptography;

namespace Core.News
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public static IConfigurationRoot Configuration { get; private set; }

        public static NewsConfiguration config { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("crypto.config.json");

            Configuration = builder.Build();
            config = NewsConfiguration.Load();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton(Configuration);
            services.AddSingleton<IWebClientService, WebClientService>();
            services.AddSingleton<INewsRepository, NewsRepository>();

            services.AddSingleton(Configuration.Get<NewsConfiguration>());
            services.AddSingleton<IEmailConfiguration>(Configuration.
                GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddTransient<IEmailService, EmailService>();

            services.AddDbContext<NewsDbContext>();
            services.AddSingleton(config);
        }
    }

}
