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
    /// Class Twitter.
    /// </summary>
    public class Twitter
    {

        /// <summary>
        /// Gets or sets the following.
        /// </summary>
        /// <value>The following.</value>
        [JsonProperty("following")]
        public int Following { get; set; }

        /// <summary>
        /// Gets or sets the account creation.
        /// </summary>
        /// <value>The account creation.</value>
        [JsonProperty("account_creation")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime AccountCreation { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the lists.
        /// </summary>
        /// <value>The lists.</value>
        [JsonProperty("lists")]
        public int Lists { get; set; }

        /// <summary>
        /// Gets or sets the statuses.
        /// </summary>
        /// <value>The statuses.</value>
        [JsonProperty("statuses")]
        public int Statuses { get; set; }

        /// <summary>
        /// Gets or sets the favourites.
        /// </summary>
        /// <value>The favourites.</value>
        [JsonProperty("favourites")]
        public int Favourites { get; set; }

        /// <summary>
        /// Gets or sets the followers.
        /// </summary>
        /// <value>The followers.</value>
        [JsonProperty("followers")]
        public int Followers { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        [JsonProperty("Points")]
        public int Points { get; set; }
    }
}
