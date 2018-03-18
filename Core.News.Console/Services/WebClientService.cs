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
        private IConfigurationRoot config;
        /// <summary>
        /// The news repository
        /// </summary>
        private readonly INewsRepository newsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebClientService"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="config">The configuration.</param>
        public WebClientService(ILoggerFactory loggerFactory, IConfigurationRoot config, INewsRepository newsRepository)
        {
            this.config = config;           
            logger = loggerFactory.CreateLogger<WebClientService>();
            this.newsRepository = newsRepository;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Start()
        {           
            Task task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    RequestLatestNews();                   
                    //Console.Title = ("Next Scan: " + DateTime.Now.AddMilliseconds(Bootstrap.Interval));
                    System.Threading.Thread.Sleep(int.Parse(config["Interval"]));
                }
            });
            task.Wait();
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
        }
        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        protected override void OnNewsComplete(object sender, NewsCompleteEventArgs e)
        {
            logger.LogInformation("Complete");
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

            var category = newsRepository.AddUpdateCategory(Map.MapProvider(e.Story.Provider));
            var content = newsRepository.AddUpdateStory(category, Map.MapStory(e.Story));
            newsRepository.AddUpdateItem(Map.MapItem(category, content));

        }
        /// <summary>
        /// Saves the stories.
        /// </summary>
        /// <param name="providers">The providers.</param>
        /// <param name="news">The news.</param>
        public void SaveStories(List<Provider> providers, List<Publication> news)
        {           
            DateTime start = DateTime.Now;

            
            //  using (var scope = new System.Transactions.TransactionScope())
            //  {
            foreach (var provider in providers)
            {
                var category = newsRepository.AddUpdateCategory(Map.MapProvider(provider));
                foreach (var story in news.Where(w => w.Source.Name == provider.Name))
                {
                    var content = newsRepository.AddUpdateStory(category, Map.MapStory(story));
                    newsRepository.AddUpdateItem(Map.MapItem(category, content));
                }
            }
            //     scope.Complete();
            //  }
            Console.Write("Duration {0}", new TimeSpan(DateTime.Now.Ticks - start.Ticks).Duration());
        }
    }

}
