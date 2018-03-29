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


using Core.News.Entities;
using Core.News.Mail;
using Core.News.Repositories;
using Crypto.Compare.ViewModels;
using News.Core.SqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.News.Services
{
    /// <summary>
    /// Class EmailRepository.
    /// </summary>
    public class EmailRepository : IEmailRepository
    {
       
        /// <summary>
        /// The news repository
        /// </summary>
        private readonly INewsRepository newsRepository;
        /// <summary>
        /// The email configuration
        /// </summary>
        private readonly IEmailConfiguration emailConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailRepository"/> class.
        /// </summary>
        /// <param name="newsRepository">The news repository.</param>
        /// <param name="emailConfiguration">The email configuration.</param>
        public EmailRepository(INewsRepository newsRepository, IEmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration.Load();
            this.newsRepository = newsRepository;
            
        }
        /// <summary>
        /// Gets the stories.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns>StoryViewModels.</returns>
        public StoryViewModels GetStories(DateTime startDate)
        {
            var stories = newsRepository.GetStoriesByDate(startDate);
            return Map.StoryView(stories);
        }
        /// <summary>
        /// Gets the configuration schedules.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public List<IGrouping<string, EmailAddress>> GetSchedules()
        {
            List<NewsSchedules> _schedules = new List<NewsSchedules>();
            return emailConfiguration.GetAddresses().GroupBy(g => g.Schedule).ToList();           
        }

        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns>List&lt;EmailAddress&gt;.</returns>
        public List<EmailAddress> GetScheduleById(string schedule)
        {
            List<NewsSchedules> _schedules = new List<NewsSchedules>();
            return emailConfiguration.GetAddresses().Where(w => w.Schedule == schedule).ToList();           
        }
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns>UserConfiguration.</returns>
        public UserConfiguration GetUsers(string schedule)
        {          
            return emailConfiguration.GetUsers(w=> w.Schedule == schedule);
        }
        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            EmailConfigContext.Save(emailConfiguration);           
        }

        /// <summary>
        /// Clones the configuration.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns>EmailConfiguration.</returns>
        public IEmailConfiguration CloneConfiguration(string schedule)
        {
            lock (emailConfiguration)
            {
                return emailConfiguration.Clone(schedule);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="EmailRepository"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get { return emailConfiguration == null ? false : emailConfiguration.Enabled; }
        }
       
    }
}
