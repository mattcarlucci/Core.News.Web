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

using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Quartz;
using News.Core.SqlServer;

namespace Core.News.Services
{
    public class EmailJobX : IJob
    {
        private readonly ILogger<EmailSchedulingService> logger;
        private readonly EmailRepository emailRepository;
        private readonly NewsRepository newsRepository;
        public EmailJobX()
        {

        }
        public EmailJobX(ILogger<EmailSchedulingService> logger, EmailRepository emailRepository, NewsRepository newsRepository)
        {
            this.logger = logger;
            this.emailRepository = emailRepository;
            this.newsRepository = newsRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
            var task = Task.Run(() =>
            {
                var key = context.JobDetail.Key.Name;
                var users = emailRepository.GetScheduleById(key);
                var date = users.Last().LastSent;

                var stories = newsRepository.GetStories(users.Last().LastSent);


                // var schedules = emailRepository.GetSchedules();
                // {
                //     foreach (var schedule in schedules)
                //     {
                //         foreach(var user in schedule)
                //         {
                //             user.LastSent = DateTime.UtcNow;
                //             logger.LogInformation("User: {0} Schedule: {1} Last: {2}", 
                //                 user.DisplayName, user.Schedule, user.LastSent);
                //         }
                //         System.Threading.Thread.Sleep(1000);
                //         emailRepository.SaveChanges();
                //     }
                //    //var client = Map.MapSmtpClient(config.Smtp);


                //    //var mail = Map.MapMailConfiguration(config, w => w.Enabled);
                //    //EmailService email = new EmailService(null);
                //    //mail.Subject = "WOW";
                //    //mail.Body = "Body";
                //    //client.Send(mail);
                //}
            });
            System.Threading.Thread.Sleep(-1);
            return task;
            //  await task;
        }
    }
}
