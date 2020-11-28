using Crypto.Compare.Extensions;
using Newtonsoft.Json;
// ***********************************************************************
// Assembly         : Crypto.Compare
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="NewsApiClient.cs" company="Crypto.Compare">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Crypto.Compare.Models;
using System.Diagnostics;
using Core.News;

namespace Crypto.Compare.Proxies
{
    /// <summary>
    /// Class NewsApiClient.
    /// </summary>
    /// <seealso cref="Crypto.News.NewsApiEvents" />
    public class NewsApiClient : NewsApiEvents, INewsApiClient, INewsApiEvents
    {
        NewsConfiguration config = null;
        /// <summary>
        /// The watch
        /// </summary>
        private Stopwatch watch = new Stopwatch();

        /// <summary>
        /// The skip details
        /// </summary>
        private bool skipDetails = false;

        /// <summary>
        /// Gets or sets the story count.
        /// </summary>
        /// <value>The story count.</value>
        public int StoryCount { get; set; }


        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        protected int StartDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the<see cref="NewsApiClient" /> class.
        /// </summary>
        public NewsApiClient() { config = NewsConfiguration.Load(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsApiClient" /> class.
        /// </summary>
        /// <param name="skipDetails">if set to <c>true</c> [skip details].</param>
        public NewsApiClient(bool skipDetails) : 
            this(skipDetails, DateTime.Now) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsApiClient"/> class.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        public NewsApiClient(DateTime startDate) : this(false, startDate) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsApiClient"/> class.
        /// </summary>
        /// <param name="skipDetails">if set to <c>true</c> [skip details].</param>
        /// <param name="startDate">The start date.</param>
        public NewsApiClient(bool skipDetails, DateTime startDate) 
        {
            config = NewsConfiguration.Load();
            this.skipDetails = skipDetails;
            StartDate = startDate.
                ToUniversalTime().ToUnixTime();
        }

      

        /// <summary>
        /// Gets the latest news.
        /// </summary>
        public void RequestLatestNews()
        {
            List<Publication> stories;

            watch.Start();
            OnNewsStart(this, StopWatchEventArgs.Create(watch));

            var filter = DateTime.Now.AddDays(-1).ToUnixTime();
            filter = StartDate;  // for debugging
            using (WebClient web = new WebClient())
            {
                stories = GetStories(web, w => int.Parse(w.publishedOn) > filter);
            }
            OnNewsComplete(this, NewsCompleteEventArgs.Create(stories, watch));
            if (stories.Count() == 0) return;
            StartDate = stories.Max(m => int.Parse(m.publishedOn));
            StoryCount = stories.Count();
        }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <param name="web">The web.</param>
        /// <returns>List&lt;Models.Provider&gt;.</returns>
        private List<Models.Provider> GetProviders(WebClient web)
        {
            try
            {
                var json = web.DownloadString(config.Urls.Provider);
                return JsonConvert.DeserializeObject<List<Models.Provider>>(json);
            }
            catch(WebException ex)
            {
                OnException(ex, new UnhandledExceptionEventArgs(ex, false));
                return new List<Provider>();
            }
        }
        /// <summary>
        /// Gets the stories.
        /// </summary>
        /// <param name="web">The web.</param>
        /// <returns>System.String.</returns>
        private List<Publication> GetStories(WebClient web)
        {
            try
            {
                var json = web.DownloadString(config.Urls.News);
                return JsonConvert.DeserializeObject<List<Publication>>(json);
            }
            catch (Exception ex)
            {
                OnException(ex, new UnhandledExceptionEventArgs(ex, false));
                return new List<Publication>();
            }
        }

        /// <summary>
        /// Gets the stories.
        /// </summary>
        /// <param name="web">The web.</param>
        /// <returns>List&lt;Models.Publication&gt;.</returns>
        private List<Publication> GetStories(WebClient web, Func<Publication, bool> filter)
        {
            var stories = GetStories(web).Where(filter).ToList();
           
            TransformStories(stories, web);

            stories.ForEach(item => OnNewsSummary(this, NewsSummaryEventArgs.Create(item, watch)));
            OnNewsSummaryComplete(this, NewsCompleteEventArgs.Create(stories, watch));
            GetStoryDetails(stories);
            return stories.OrderByDescending(order => order.publishedOn).ToList();
        }

        /// <summary>
        /// Gets the story details.
        /// </summary>
        /// <param name="stories">The stories.</param>
        /// <returns>List&lt;Publication&gt;.</returns>
        private List<Publication> GetStoryDetails(List<Publication> stories)
        {
            if (skipDetails) return stories;

            Parallel.ForEach(stories.OrderBy(o => int.Parse(o.publishedOn)), story =>
            {
                try
                {
                    using (WebClient cli = new WebClient())
                        story.UrlData = cli.DownloadString(story.Url);

                    OnNewsDetail(this, NewsDetailEventArgs.Create(story, watch));
                }
                catch(Exception)
                {      
                    //TODO: need to log errors
                    return;
                }
            });

            OnNewsDetailComplete(this, NewsCompleteEventArgs.Create(stories, watch));
            return stories;
        }

        /// <summary>
        /// Transforms the stories.
        /// </summary>
        /// <param name="stories">The stories.</param>
        /// <param name="web">The web.</param>
        private void TransformStories(List<Publication> stories, WebClient web)
        {
            if (stories.Count == 0) return;

            var providers = GetProviders(web);
            //var anchor = "<a href=\"{0}/\"> Read More </a>";
            if (providers.Count == 0)
            {
                stories.Clear();
                return;
            }

            Parallel.ForEach(stories.OrderBy(o => int.Parse(o.publishedOn)), story =>
            {               
                story.Title = story.Title.Ascii();
                story.Body = story.Body.Ascii();// + string.Format(anchor, story.Url);
                story.Provider = providers.FirstOrDefault(s => s.Name == story.Source.Name);                
            });
        }      
    }
}
