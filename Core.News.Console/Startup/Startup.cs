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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using News.Core.SqlServer;
using News.Core.SqlServer.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.IO;
using Core.News.Cryptography;

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
            AddLogging();
        }

        /// <summary>
        /// Add logging.
        /// </summary>
        private static void AddLogging()
        {
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.Trace()
            //    .WriteTo.RollingFile("Logs/trace.log", Serilog.Events.LogEventLevel.Debug).CreateLogger();

           // Log.Logger = new LoggerConfiguration()
           //     .WriteTo.Trace()
           //     .WriteTo.ColoredConsole(Serilog.Events.LogEventLevel.Verbose)           
           //     .WriteTo.RollingFile("Logs/Core.News.Console-{Date}.log", Serilog.Events.LogEventLevel.Verbose)
           //.CreateLogger();

           // Log.Logger.Information("Logging initialized...");
        }
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    })
                .PersistKeysToFileSystem(new DirectoryInfo(@".\keys"))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(365));                       

            services.AddLogging();
            services.AddSingleton(Configuration);
            
            services.AddSingleton<INewsRepository, NewsRepository>();
            services.AddSingleton<IEmailRepository, EmailRepository>();
            services.AddSingleton<IWebClientService, WebClientService>();
            services.AddSingleton<IEmailConfiguration, EmailConfiguration>();
            services.AddSingleton<IEmailSchedulingService, EmailSchedulingService>();
            services.AddSingleton<ICipherService, CipherService>();
            services.AddSingleton<ICipherKeyProvider, EmailKeyProvider>();
            
            services.UseQuartz(typeof(EmailJob), typeof(PerfJob));

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
