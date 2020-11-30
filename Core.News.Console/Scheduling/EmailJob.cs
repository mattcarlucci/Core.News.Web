// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-24-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-24-2018
// ***********************************************************************
// <copyright file="EmailJob.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News.Repositories;
using Core.News.Services;
using CronExpressionDescriptor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using News.Core.SqlServer;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.News.Console.Scheduling
{

    /// <summary>
    /// Class EmailJob.
    /// </summary>
    /// <seealso cref="Quartz.IJob" />
    public class EmailJob : IJob
    {
        /// <summary>
        /// The synchronize lock
        /// </summary>
        static object syncLock = new object();
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<EmailJob> logger;
        /// <summary>
        /// The email repository
        /// </summary>
        private readonly IEmailRepository emailRepository;
        /// <summary>
        /// The news repository
        /// </summary>
        private readonly INewsRepository newsRepository;
        /// <summary>
        /// The context
        /// </summary>
        private IJobExecutionContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailJob"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailRepository">The email repository.</param>
        /// <param name="newsRepository">The news repository.</param>
        public EmailJob(ILogger<EmailJob> logger, IEmailRepository emailRepository, INewsRepository newsRepository)
        {
            this.logger = logger;
            this.emailRepository = emailRepository;
            this.newsRepository = newsRepository;
        }

        /// <summary>
        /// Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.ITrigger" />
        /// fires that is associated with the <see cref="T:Quartz.IJob" />.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <returns>Task.</returns>
        /// <remarks>The implementation may wish to set a  result object on the
        /// JobExecutionContext before this method exits.  The result itself
        /// is meaningless to Quartz, but may be informative to
        /// <see cref="T:Quartz.IJobListener" />s or
        /// <see cref="T:Quartz.ITriggerListener" />s that are watching the job's
        /// execution.</remarks>
        public Task Execute(IJobExecutionContext context)
        {
           this.context = context;
           return  Task.Run(Execute);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task Execute()
        {
            try
            {               
                
                lock (syncLock)
                {
                    DateTime offset = DateTime.UtcNow.AddYears(-1);

                    var key = context.JobDetail.Key.Name;
                    var group = context.JobDetail.Key.Group;
                    var users = emailRepository.GetScheduleById(group);

                    if (users == null)
                        return; //no one to send this to

                    if (users.Count > 0)
                        offset = users.Last().LastSent;

                    var stories = newsRepository.GetStoriesByDate(offset.AddDays(0)).ToList();
                    if (stories.Count == 0) return;

                    var userList = emailRepository.GetUsers(group);
                    var config = emailRepository.CloneConfiguration(group);

                    var mail = Map.MailConfiguration(config, w => w.Enabled);
                    mail.Body = Map.StoryView(stories).MailMessage();
                    mail.Subject = "Crypto News Alert";
                                       
                    logger.LogInformation("Executing job {0} - Last Scan: {1} - Users: {2} - Stories: {3}",
                        CronExprs.GetDescription(context.JobDetail.Key.Name), offset, users.Count, stories.Count);

                    logger.LogInformation("Emailing {0} stories {1}", stories.Count, userList);

                    if (config.SmtpClient == "RestSmtp")
                    {
                        var response = RestEmail.SendEmail(mail, config.RestSmtp);
                        logger.LogInformation($"Rest Call ResponseCode: {response?.StatusCode}");
                    }
                    else
                    {
                        using (SmtpClient client = Map.SmtpClient(config.Smtp))
                        {
                            logger.LogInformation("Emailing {0} stories {1}", stories.Count, userList);
                            client.Send(mail);
                            logger.LogInformation("Email Sent!");
                        }
                    }
                    logger.LogInformation("Email Sent!");

                    users.ForEach(user => user.LastSent = DateTime.UtcNow);
                    emailRepository.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "An error occurred during execution of scheduled job");
            }
            await Task.FromResult(0);
        }
    }
}
