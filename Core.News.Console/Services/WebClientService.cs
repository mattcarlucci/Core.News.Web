// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="WebClientService.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Crypto.Compare.Proxies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using Crypto.Compare;
using Crypto.Compare.Extensions;
using System.Collections.Generic;
using Crypto.Compare.Models;
using System.Threading;
using Core.News.Services;

namespace Core.News
{
    /// <summary>
    /// Class WebClientService.
    /// </summary>
    /// <seealso cref="Crypto.Compare.Proxies.WebApiClient" />
    /// <seealso cref="Core.News.IWebClientService" />
    public class WebClientService : WebApiClient, IWebClientService
    {        
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<WebClientService> logger;
        /// <summary>
        /// The configuration
        /// </summary>
        private IConfigurationRoot configuration;
        /// <summary>
        /// The news repository
        /// </summary>
        private readonly INewsRepository newsRepository;
        /// <summary>
        /// The news configuration
        /// </summary>
        private readonly NewsConfiguration newsConfiguration;

        /// <summary>
        /// The email service
        /// </summary>
        private readonly IEmailService emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebClientService"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">The configuration.</param>
        public WebClientService(ILoggerFactory loggerFactory, IConfigurationRoot configuration, 
            INewsRepository newsRepository, NewsConfiguration newsConfiguration, IEmailService emailService)
        {
            this.configuration = configuration;           
            this.logger = loggerFactory.CreateLogger<WebClientService>();
            this.newsRepository = newsRepository;
            this.newsConfiguration = newsConfiguration;
            this.emailService = emailService;
        }
        
        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                if (!DateTime.TryParse(this.newsConfiguration.IntervalStart, out DateTime start)) break;
                                
                TimeSpan span = new TimeSpan(start.Ticks - DateTime.Now.Ticks);
                logger.LogInformation("Waiting {0} to begin", span.Duration());
                if (span.TotalMilliseconds <= 0) break;
                Thread.Sleep(10000);
            }

            StartDate = newsRepository.GetLastContentDate().ToUnixTime();
            Task  task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    RequestLatestNews();                   
                    Thread.Sleep(newsConfiguration.Interval * 1000 * 60);
                }
            });         
            await task;
        }
       
        /// <summary>
        /// Handles the <see cref="E:NewsEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:Crypto.Compare.Proxies.NewsSummaryEventArgs" /> instance containing the event data.</param>
        protected override void OnNewsSummary(object sender, NewsSummaryEventArgs e)
        {
            logger.LogInformation("{0}", e.Story.Headline);
        }
        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        protected override void OnNewsStart(object sender, StopWatchEventArgs e)
        {
            logger.LogInformation("Started...");
        }
        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        protected override void OnNewsSummaryComplete(object sender, NewsCompleteEventArgs e)
        {
            logger.LogInformation("Summary Complete {0} stories", e.Stories.Count());
            var model = Map.StoryView(e.Stories);
            if (e.Stories.Count == 0) return;

            emailService.Send("Crypto News Alert", model.MailMessage());
        }
        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        protected override void OnNewsComplete(object sender, NewsCompleteEventArgs e)
        {
            logger.LogInformation("Complete");

            logger.LogInformation("Next Scan: {0:MM/dd/yyyy hh:mm tt}", DateTime.Now.
                AddMinutes(newsConfiguration.Interval));
        }
        /// <summary>
        /// Handles the <see cref="E:NewsDetailEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:Crypto.Compare.Proxies.NewsCompleteEventArgs" /> instance containing the event data.</param>
        protected override void OnNewsDetailComplete(object sender, NewsCompleteEventArgs e)
        {
            logger.LogInformation("Detail Complete");
        }
        /// <summary>
        /// Handles the <see cref="E:NewsEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:Crypto.Compare.Proxies.NewsSummaryEventArgs" /> instance containing the event data.</param>
        protected override void OnNewsDetail(object sender, NewsDetailEventArgs e)
        {
            //newsRepository.AddUpdateCategory(e.)
            logger.LogInformation("Read Details {0} bytes", e.Story.UrlData.Count());

            lock (logger)
            {
                var category = newsRepository.AddUpdateCategory(Map.Provider(e.Story.Provider));
                var content = newsRepository.AddUpdateStory(category, Map.Story(e.Story));
                newsRepository.AddUpdateItem(Map.Item(category, content));
            }
        }        
    }
}
