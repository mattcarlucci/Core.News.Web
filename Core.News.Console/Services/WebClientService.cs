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
using Core.News.Repositories;
using Microsoft.Extensions.Hosting;
using News.Core.SqlServer;

namespace Core.News
{
    /// <summary>
    /// Class WebClientService.
    /// </summary>
    /// <seealso cref="Crypto.Compare.Proxies.NewsApiClient" />
    /// <seealso cref="Core.News.IWebClientService" />
    public class WebClientService : NewsApiClient, IHostedService, IWebClientService
    {

        /// <summary>
        /// The cancellation token
        /// </summary>
        CancellationToken cancellationToken;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<WebClientService> logger;
        /// <summary>
        /// The news repository
        /// </summary>
        private readonly INewsRepository newsRepository;
        /// <summary>
        /// The news configuration
        /// </summary>
        private readonly NewsConfiguration newsConfiguration;
        /// <summary>
        /// Initializes a new instance of the <see cref="WebClientService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="newsRepository">The news repository.</param>
        /// <param name="newsConfiguration">The news configuration.</param>
        public WebClientService(ILogger<WebClientService> logger, INewsRepository newsRepository, 
            NewsConfiguration newsConfiguration)
        {
            this.logger = logger;
            this.newsRepository = newsRepository;
            this.newsConfiguration = newsConfiguration;            
        }      
        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns>Task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //TODO: replace this with Quartz Job
            this.cancellationToken = cancellationToken;
            StartDate = newsRepository.GetLastContentDate().ToUnixTime();
            Task  task = Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    RequestLatestNews();                   
                    Thread.Sleep((int)Math.Round(newsConfiguration.Interval * 1000 * 60, 0));
                }
            });
            return task;       
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

           // emailService.Send("Crypto News Alert", model.MailMessage());
        }
        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        protected override void OnNewsComplete(object sender, NewsCompleteEventArgs e)
        {
            logger.LogInformation("Complete");

            logger.LogInformation("Next Scan: {0}", DateTime.Now.
                AddMinutes(newsConfiguration.Interval).ToLongTimeString());
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
        /// <summary>
        /// Handles the <see cref="E:Exception" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.UnhandledExceptionEventArgs" /> instance containing the event data.</param>
        protected override void OnException(object sender, UnhandledExceptionEventArgs e)
        {           
            logger.LogError(e.ExceptionObject as Exception, "WebClientApi returned with an exception");
            
            if (GlobalExtensions.IsWindows())
                System.Console.Beep(250, 200);

            //  e = errorCounter++ == 10 ? e = new UnhandledExceptionEventArgs(e.ExceptionObject, true) : e;
            base.OnException(sender, e);

        }
        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>Task.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            return Task.FromResult(0);
        }
    }
}
