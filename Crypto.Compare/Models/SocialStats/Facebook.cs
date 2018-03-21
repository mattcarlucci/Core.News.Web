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
using Newtonsoft.Json;

namespace Crypto.Compare.Models.SocialStats
{
    /// <summary>
    /// Class Facebook.
    /// </summary>
    public class Facebook
    {

        /// <summary>
        /// Gets or sets the likes.
        /// </summary>
        /// <value>The likes.</value>
        [JsonProperty("likes")]
        public int Likes { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the is closed.
        /// </summary>
        /// <value>The is closed.</value>
        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        /// <summary>
        /// Gets or sets the talking about.
        /// </summary>
        /// <value>The talking about.</value>
        [JsonProperty("talking_about")]
        public int TalkingAbout { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        [JsonProperty("Points")]
        public int Points { get; set; }
    }
}
