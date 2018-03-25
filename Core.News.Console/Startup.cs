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
using Core.News.Console.Scheduling;
using Core.News.Mail;
using Core.News.Repositories;
using Core.News.Services;
using Crypto.Compare.Proxies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using News.Core.SqlServer;
using News.Core.SqlServer.Models;
using System;
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
        private static IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private static NewsConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(NewsConfiguration.configFile);

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
            
            services.AddSingleton<INewsRepository, NewsRepository>();
            services.AddSingleton<IEmailRepository, EmailRepository>();

            services.AddSingleton<IWebClientService, WebClientService>();
            services.AddSingleton<IEmailConfiguration, EmailConfiguration>();
            services.AddSingleton<IEmailSchedulingService, EmailSchedulingService>();

            services.UseQuartz<EmailJob>();

            services.AddEntityFrameworkSqlServer();
            ServiceProvider dbProvider = services.AddScoped<DbContext>(provider => 
            provider.GetService<NewsDbContext>())
                .AddDbContext<NewsDbContext>((provider, options) =>
                {
                    options.UseSqlServer(config.GetDefaultConnection().Value);
                    options.UseInternalServiceProvider(provider);
                })
                .BuildServiceProvider(); 

            services.AddSingleton(dbProvider);
            services.AddSingleton(config);          
        }       
    }
}
