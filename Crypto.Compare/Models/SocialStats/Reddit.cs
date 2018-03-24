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
    /// Class Reddit.
    /// </summary>
    public class Reddit
    {

        /// <summary>
        /// Gets or sets the posts per hour.
        /// </summary>
        /// <value>The posts per hour.</value>
        [JsonProperty("posts_per_hour")]
        public double PostsPerHour { get; set; }

        /// <summary>
        /// Gets or sets the comments per hour.
        /// </summary>
        /// <value>The comments per hour.</value>
        [JsonProperty("comments_per_hour")]
        public double CommentsPerHour { get; set; }

        /// <summary>
        /// Gets or sets the posts per day.
        /// </summary>
        /// <value>The posts per day.</value>
        [JsonProperty("posts_per_day")]
        public double PostsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the comments per day.
        /// </summary>
        /// <value>The comments per day.</value>
        [JsonProperty("comments_per_day")]
        public double CommentsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the active users.
        /// </summary>
        /// <value>The active users.</value>
        [JsonProperty("active_users")]
        public int ActiveUsers { get; set; }

        /// <summary>
        /// Gets or sets the community creation.
        /// </summary>
        /// <value>The community creation.</value>
        [JsonProperty("community_creation")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime CommunityCreation { get; set; }

        /// <summary>
        /// Gets or sets the subscribers.
        /// </summary>
        /// <value>The subscribers.</value>
        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        [JsonProperty("Points")]
        public int Points { get; set; }
    }
}
