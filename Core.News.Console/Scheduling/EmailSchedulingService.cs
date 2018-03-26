// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="EmailService.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Linq;
using Microsoft.Extensions.Logging;
using Core.News.Mail;
using Microsoft.Extensions.DependencyInjection;
using Core.News.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using News.Core.SqlServer;
using Core.News.Console.Scheduling;

namespace Core.News.Services
{
    public class EmailSchedulingService : IEmailSchedulingService
    {
        private readonly ILogger<EmailSchedulingService> logger;
        private readonly IEmailRepository emailRepository;
        private readonly INewsRepository newsRepository;

        /// <summary>
        /// The scheduler
        /// </summary>
        IScheduler scheduler;
       // <summary>
        /// Initializes a new instance of the <see cref="EmailSchedulingService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailRepository">The email repository.</param>
        public EmailSchedulingService(ILogger<EmailSchedulingService> logger, 
            IEmailRepository emailRepository, INewsRepository newsRepository, IScheduler scheduler)
        {
            this.logger = logger;
            this.emailRepository = emailRepository;
            this.newsRepository = newsRepository;
            this.scheduler = scheduler;
            QuartzServicesUtilities.StartJob<PerfJob>(scheduler, new System.TimeSpan(1, 0, 0));            
        }

        /// <summary>
        /// Starts the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        public void CreateJobs()
        {
            Dictionary<string, string> Keys = CronExprs.GetPairs();
                
            emailRepository.GetSchedules().Select(s => s.Key).ToList().ForEach(item =>
            {
                string expr = item;
                logger.LogInformation("Adding Schedule for {0}", item);
                if (Keys.ContainsKey(expr)) expr = Keys[expr];

                IJobDetail job = JobBuilder.Create<EmailJob>()
               .WithIdentity(expr, item).Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity(expr, item).WithCronSchedule(expr)
                    .ForJob(expr, item).Build();

                scheduler.ScheduleJob(job, trigger);               
            }); 
        }     
    }
}
