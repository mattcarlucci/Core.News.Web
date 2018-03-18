// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-18-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-18-2018
// ***********************************************************************
// <copyright file="SchedulerExtensions.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.News.Services
{
    /// <summary>
    /// Class SchedulerExtensions.
    /// </summary>
    public static class SchedulerExtensions
    {
        /// <summary>
        /// Adds the scheduler.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddScheduler(this IServiceCollection services)
        {
            return services.AddSingleton<IHostedService, SchedulerHostedService>();
        }

        /// <summary>
        /// Adds the scheduler.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="unobservedTaskExceptionHandler">The unobserved task exception handler.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddScheduler(this IServiceCollection services, EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskExceptionHandler)
        {
            return services.AddSingleton<IHostedService, SchedulerHostedService>(serviceProvider =>
            {
                var instance = new SchedulerHostedService(
                    serviceProvider.GetServices<IScheduledTask>(), 
                    serviceProvider.GetService<ILoggerFactory>()
                    );

                instance.UnobservedTaskException += unobservedTaskExceptionHandler;
                return instance;
            });
        }
    }
}
