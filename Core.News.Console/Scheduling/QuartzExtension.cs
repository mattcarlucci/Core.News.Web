// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-24-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-24-2018
// ***********************************************************************
// <copyright file="QuartzExtension.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace Core.News.Console.Scheduling
{
    /// <summary>
    /// Class QuartzExtension.
    /// </summary>
    public static class QuartzExtension
    {
        /// <summary>
        /// Uses the quartz.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="jobs">The jobs.</param>
        public static void UseQuartz<T>(this IServiceCollection services, params Type[] jobs) where T: IJob
        {
            services.UseQuartz(typeof(T));
        }

        /// <summary>
        /// Uses the quartz.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="jobs">The jobs.</param>
        public static void UseQuartz(this IServiceCollection services, params Type[] jobs)
        {
            services.AddSingleton<IJobFactory, QuartzJobFactory>();

            foreach (var job in jobs)
                services.Add(new ServiceDescriptor(job, job, ServiceLifetime.Singleton));

            services.AddSingleton(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
             
                return scheduler;
            });
        }
    }
}
