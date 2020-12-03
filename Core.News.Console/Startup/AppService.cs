// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 12-03-2020
// ***********************************************************************
// <copyright file="Service.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using AutoMapper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

using Core.News.Services;
using Core.News.Console.Scheduling;

namespace Core.News
{
    /// <summary>
    /// Class Bootstrap.
    /// </summary>
    public class AppService: BackgroundService
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<AppService> logger;

        /// <summary>
        /// The schedule
        /// </summary>
        private readonly IEmailSchedulingService scheduler;

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>The mapper.</value>
        public static IMapper Mapper => AutoMapperConfig.Instance;


        /// <summary>
        /// Initializes a new instance of the <see cref="AppService"/> class.
        /// </summary>
        public AppService()
        {
            IServiceCollection services = new ServiceCollection();

            Startup startup = new Startup();
            startup.ConfigureServices(services);

            services.AddLogging(config =>
            {
                config.AddConsole().SetMinimumLevel(LogLevel.Information);
                config.AddFile("Logs/Core.News.Console-{Date}.log").SetMinimumLevel(LogLevel.Information);
            });

            //TODO: Start using Serilog
            Log.Logger = new LoggerConfiguration()
           .WriteTo.RollingFile("Logs/trace.log", Serilog.Events.LogEventLevel.Verbose).CreateLogger();

            AutoMapperConfig.RegisterMappings();

            serviceProvider = services.BuildServiceProvider();
            logger = serviceProvider.GetService<ILogger<AppService>>();
            scheduler = serviceProvider.GetService<IEmailSchedulingService>();
        }

        /// <summary>
        /// execute as an asynchronous operation.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug(PerfJob.GetProcessInfo());
            try
            {
                scheduler.CreateJobs();
                IWebClientService webClient = serviceProvider.GetService<IWebClientService>();
                await webClient.StartAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, ex.Message);
                scheduler.Shutdown();                
                logger.LogDebug(PerfJob.GetProcessInfo());
            }
        }
        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>Task.</returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Service started...");
            return base.StartAsync(cancellationToken);
        }
        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>Task.</returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Service stopping...");
            return base.StopAsync(cancellationToken);
        }
    }
}
