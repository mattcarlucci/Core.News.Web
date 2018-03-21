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
using System;
using System.Text;

namespace Crypto.Compare.Models.SocialStats
{

    /// <summary>
    /// Class SocialStats.
    /// </summary>
    public class SocialStats
    {

        /// <summary>
        /// Gets or sets the general.
        /// </summary>
        /// <value>The general.</value>
        [JsonProperty("General")]
        public General General { get; set; }

        /// <summary>
        /// Gets or sets the crypto compare.
        /// </summary>
        /// <value>The crypto compare.</value>
        [JsonProperty("CryptoCompare")]
        public CryptoCompare CryptoCompare { get; set; }

        /// <summary>
        /// Gets or sets the twitter.
        /// </summary>
        /// <value>The twitter.</value>
        [JsonProperty("Twitter")]
        public Twitter Twitter { get; set; }

        /// <summary>
        /// Gets or sets the reddit.
        /// </summary>
        /// <value>The reddit.</value>
        [JsonProperty("Reddit")]
        public Reddit Reddit { get; set; }

        /// <summary>
        /// Gets or sets the facebook.
        /// </summary>
        /// <value>The facebook.</value>
        [JsonProperty("Facebook")]
        public Facebook Facebook { get; set; }

        /// <summary>
        /// Gets or sets the code repository.
        /// </summary>
        /// <value>The code repository.</value>
        [JsonProperty("CodeRepository")]
        public CodeRepository CodeRepository { get; set; }
    }
}
