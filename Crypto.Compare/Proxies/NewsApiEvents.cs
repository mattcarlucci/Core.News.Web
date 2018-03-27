// ***********************************************************************
// Assembly         : Crypto.Compare
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="NewsApiEvents.cs" company="Crypto.Compare">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Crypto.Compare.Proxies
{
    /// <summary>
    /// Class NewsApiEvents.
    /// </summary>
    public class NewsApiEvents : INewsApiEvents
    {
        /// <summary>
        /// The start
        /// </summary>
        /// <value>The news start.</value>
        public EventHandler<StopWatchEventArgs> NewsStart { get; set; }

        /// <summary>
        /// The news event
        /// </summary>
        /// <value>The news summary.</value>
        public EventHandler<NewsSummaryEventArgs> NewsSummary { get; set; }
  
        /// <summary>
        /// Gets or sets the news detail event.
        /// </summary>
        /// <value>The news detail event.</value>
        public EventHandler<NewsDetailEventArgs> NewsDetail { get; set; }

        /// <summary>
        /// Gets or sets the news summary complete event.
        /// </summary>
        /// <value>The news summary complete event.</value>
        public EventHandler<NewsCompleteEventArgs> NewsSummaryComplete { get; set; }

        /// <summary>
        /// Gets the news detail event complete.
        /// </summary>
        /// <value>The news detail event complete.</value>
        public EventHandler<NewsCompleteEventArgs> NewsDetailComplete { get; set; }

        /// <summary>
        /// Gets or sets the news event complete.
        /// </summary>
        /// <value>The news event complete.</value>
        public EventHandler<NewsCompleteEventArgs> NewsComplete { get; set; }


        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public UnhandledExceptionEventHandler Exception { get;set; }

        /// <summary>
        /// Handles the <see cref="E:Exception" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception?.Invoke(sender, e);
        }
        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnNewsStart(object sender, StopWatchEventArgs e)
        {
            NewsStart?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the <see cref="E:NewsEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NewsSummaryEventArgs" /> instance containing the event data.</param>
        protected virtual void OnNewsSummary(object sender, NewsSummaryEventArgs e)
        {
            NewsSummary?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the <see cref="E:NewsEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NewsSummaryEventArgs" /> instance containing the event data.</param>
        protected virtual void OnNewsDetail(object sender, NewsDetailEventArgs e)
        {
            NewsDetail?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnNewsSummaryComplete(object sender, NewsCompleteEventArgs e)
        {
            NewsSummaryComplete?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the <see cref="E:NewsDetailEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NewsCompleteEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void OnNewsDetailComplete(object sender, NewsCompleteEventArgs e)
        {
            NewsDetailComplete?.Invoke(sender, e);
        }
      

        /// <summary>
        /// Handles the <see cref="E:NewsEventComplete" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnNewsComplete(object sender, NewsCompleteEventArgs e)
        {
            NewsComplete?.Invoke(sender, e);
        }
       

    }
}
