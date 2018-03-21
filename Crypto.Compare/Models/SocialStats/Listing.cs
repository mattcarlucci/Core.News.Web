// ***********************************************************************
// Assembly         : Crypto.Compare
// Author           : mcarlucci
// Created          : 03-20-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-20-2018
// ***********************************************************************
// <copyright file="SocialStats.cs" company="Crypto.Compare">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News.Converters;
using Newtonsoft.Json;
using System;

namespace Crypto.Compare.Models.SocialStats
{
    /// <summary>
    /// Class Listing.
    /// </summary>
    public class Listing
    {

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the open total issues.
        /// </summary>
        /// <value>The open total issues.</value>
        [JsonProperty("open_total_issues")]
        public int OpenTotalIssues { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        [JsonProperty("parent")]
        public Parent Parent { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the closed total issues.
        /// </summary>
        /// <value>The closed total issues.</value>
        [JsonProperty("closed_total_issues")]
        public int ClosedTotalIssues { get; set; }

        /// <summary>
        /// Gets or sets the stars.
        /// </summary>
        /// <value>The stars.</value>
        [JsonProperty("stars")]
        public int Stars { get; set; }

        /// <summary>
        /// Gets or sets the last update.
        /// </summary>
        /// <value>The last update.</value>
        [JsonProperty("last_update")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the forks.
        /// </summary>
        /// <value>The forks.</value>
        [JsonProperty("forks")]
        public int Forks { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the closed issues.
        /// </summary>
        /// <value>The closed issues.</value>
        [JsonProperty("closed_issues")]
        public int ClosedIssues { get; set; }

        /// <summary>
        /// Gets or sets the closed pull issues.
        /// </summary>
        /// <value>The closed pull issues.</value>
        [JsonProperty("closed_pull_issues")]
        public int ClosedPullIssues { get; set; }

        /// <summary>
        /// Gets or sets the fork.
        /// </summary>
        /// <value>The fork.</value>
        [JsonProperty("fork")]
        public bool IsFork { get; set; }

        /// <summary>
        /// Gets or sets the last push.
        /// </summary>
        /// <value>The last push.</value>
        [JsonProperty("last_push")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime LastPush { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty("source")]
        public Source Source { get; set; }

        /// <summary>
        /// Gets or sets the open pull issues.
        /// </summary>
        /// <value>The open pull issues.</value>
        [JsonProperty("open_pull_issues")]
        public int OpenPullIssues { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the subscribers.
        /// </summary>
        /// <value>The subscribers.</value>
        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        /// <summary>
        /// Gets or sets the open issues.
        /// </summary>
        /// <value>The open issues.</value>
        [JsonProperty("open_issues")]
        public int OpenIssues { get; set; }
    }
}
