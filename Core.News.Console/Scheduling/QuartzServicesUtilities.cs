// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-24-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-24-2018
// ***********************************************************************
// <copyright file="QuartzServicesUtilities.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Quartz;
using System;

namespace Core.News.Console.Scheduling
{
    /// <summary>
    /// Class QuartzServicesUtilities.
    /// </summary>
    public static class QuartzServicesUtilities
    {
        /// <summary>
        /// Starts the job.
        /// </summary>
        /// <typeparam name="TJob">The type of the t job.</typeparam>
        /// <param name="scheduler">The scheduler.</param>
        /// <param name="runInterval">The run interval.</param>
        public static void StartJob<TJob>(IScheduler scheduler, TimeSpan runInterval)
            where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()
                .WithSimpleSchedule(scheduleBuilder =>
                    scheduleBuilder
                        .WithInterval(runInterval)
                        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }    
    }
}
