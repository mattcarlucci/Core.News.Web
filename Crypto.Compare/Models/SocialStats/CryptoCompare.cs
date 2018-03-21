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
using System.Collections.Generic;

namespace Crypto.Compare.Models.SocialStats
{
    /// <summary>
    /// Class CryptoCompare.
    /// </summary>
    public class CryptoCompare
    {

        /// <summary>
        /// Gets or sets the similar items.
        /// </summary>
        /// <value>The similar items.</value>
        [JsonProperty("SimilarItems")]
        public IList<SimilarItem> SimilarItems { get; set; }

        /// <summary>
        /// Gets or sets the cryptopian followers.
        /// </summary>
        /// <value>The cryptopian followers.</value>
        [JsonProperty("CryptopianFollowers")]
        public IList<CryptopianFollower> CryptopianFollowers { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        [JsonProperty("Points")]
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets the followers.
        /// </summary>
        /// <value>The followers.</value>
        [JsonProperty("Followers")]
        public int Followers { get; set; }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        /// <value>The posts.</value>
        [JsonProperty("Posts")]
        public int Posts { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        [JsonProperty("Comments")]
        public int Comments { get; set; }

        /// <summary>
        /// Gets or sets the page views split.
        /// </summary>
        /// <value>The page views split.</value>
        [JsonProperty("PageViewsSplit")]
        public PageViewsSplit PageViewsSplit { get; set; }

        /// <summary>
        /// Gets or sets the page views.
        /// </summary>
        /// <value>The page views.</value>
        [JsonProperty("PageViews")]
        public int PageViews { get; set; }
    }
}
