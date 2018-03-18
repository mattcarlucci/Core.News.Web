// ***********************************************************************
// Assembly         : Crypto.Compare
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="NewsEventArgs.cs" company="Crypto.Compare">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Crypto.Compare.Models;

namespace Crypto.Compare.Proxies
{
    /// <summary>
    /// Class NewsCompleteEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class NewsCompleteEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public List<Publication> Stories { get; set; }
        /// <summary>
        /// Gets or sets the watch.
        /// </summary>
        /// <value>The watch.</value>
        public Stopwatch Watch { get; set; }

        /// <summary>
        /// Creates the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="watch">The watch.</param>
        /// <returns>NewsCompleteEventArgs.</returns>
        public static NewsCompleteEventArgs Create(List<Publication> stories, Stopwatch watch)
        {
            return new NewsCompleteEventArgs(stories, watch);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsCompleteEventArgs" /> class.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="watch">The watch.</param>
        public NewsCompleteEventArgs(List<Publication> stories, Stopwatch watch)
        {
            this.Stories = stories;
            Watch = watch;
        }
    }
    
    /// <summary>
    /// Class NewsEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class NewsSummaryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the stories.
        /// </summary>
        /// <value>The stories.</value>
        public Publication Story { get; set; }

        /// <summary>
        /// Gets or sets the watch.
        /// </summary>
        /// <value>The watch.</value>
        public Stopwatch Watch { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsSummaryEventArgs" /> class.
        /// </summary>
        /// <param name="story">The story.</param>
        public NewsSummaryEventArgs(Models.Publication story)
        {
            this.Story = story;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsSummaryEventArgs" /> class.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <param name="watch">The watch.</param>
        public NewsSummaryEventArgs(Models.Publication story, Stopwatch watch)
        {
            this.Story = story;
            this.Watch = watch;
        }

        /// <summary>
        /// Creates the specified stories.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <returns>NewsEventArgs.</returns>
        public static NewsSummaryEventArgs Create(Publication story)
        {
            return new NewsSummaryEventArgs(story);
        }

        /// <summary>
        /// Creates the specified story.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <param name="watch">The watch.</param>
        /// <returns>NewsDetailEvent.</returns>
        public static NewsSummaryEventArgs Create(Publication story, Stopwatch watch)
        {
            return new NewsSummaryEventArgs(story, watch);
        }
    }
    /// <summary>
    /// Class NewsEventDetailArgs.
    /// </summary>
    /// <seealso cref="Crypto.Compare.Proxies.NewsSummaryEventArgs" />
    public class NewsDetailEventArgs : NewsSummaryEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsDetailEvent" /> class.
        /// </summary>
        /// <param name="story">The story.</param>
        public NewsDetailEventArgs(Publication story) 
            : base(story)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsDetailEvent" /> class.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <param name="watch">The watch.</param>
        public NewsDetailEventArgs(Publication story, Stopwatch watch) 
            : base(story, watch)
        {
        }
        /// <summary>
        /// Creates the specified stories.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <returns>NewsEventArgs.</returns>
        public new static NewsDetailEventArgs Create(Publication story)
        {
            return new NewsDetailEventArgs(story);
        }

        /// <summary>
        /// Creates the specified story.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <param name="watch">The watch.</param>
        /// <returns>NewsDetailEvent.</returns>
        public new static NewsDetailEventArgs Create(Publication story, Stopwatch watch)
        {
            return new NewsDetailEventArgs(story, watch);
        }
    }

  
}
