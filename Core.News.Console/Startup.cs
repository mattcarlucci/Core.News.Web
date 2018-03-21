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
using Core.News.Mail;
using Core.News.Repositories;
using Core.News.Services;
using Crypto.Compare.Proxies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton(Configuration.Get<NewsConfiguration>());

            services.AddSingleton<IEmailConfiguration>(Configuration.
                GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddSingleton<IEmailService, EmailService>();

            //services.AddSingleton<QuoteProvider>();
            //services.AddSingleton<IHostedService, QuoteService>();

            //TODO: Will add other db types. ie Sqlite EF            
            //services.AddDbContext<NewsDbContext>();

            services.AddEntityFrameworkSqlServer();

            ServiceProvider sp = services.AddScoped<DbContext>(provider => 
            provider.GetService<NewsDbContext>())
            .AddDbContext<NewsDbContext>((provider, options) =>
            {
                options.UseSqlServer(config.GetDefaultConnection().Value);
                options.UseInternalServiceProvider(provider);
            })
            .BuildServiceProvider();                     

            services.AddSingleton(sp);
       
            services.AddSingleton(config);     
            services.AddSingleton<IScheduledTask, QuoteProviderMock>();
            services.AddSingleton<IWebClientService, WebClientService>();

            services.AddScheduler((sender, args) =>
            {              
                Console.Write(args.Exception.Message);
                args.SetObserved();
            });

            //should we run the migration each time?
            using (var db = sp.GetService<NewsDbContext>())
                db.Database.Migrate();
        }
    }
}
