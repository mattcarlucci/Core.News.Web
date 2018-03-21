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
    /// Class PageViewsSplit.
    /// </summary>
    public class PageViewsSplit
    {

        /// <summary>
        /// Gets or sets the overview.
        /// </summary>
        /// <value>The overview.</value>
        [JsonProperty("Overview")]
        public int Overview { get; set; }

        /// <summary>
        /// Gets or sets the markets.
        /// </summary>
        /// <value>The markets.</value>
        [JsonProperty("Markets")]
        public int Markets { get; set; }

        /// <summary>
        /// Gets or sets the analysis.
        /// </summary>
        /// <value>The analysis.</value>
        [JsonProperty("Analysis")]
        public int Analysis { get; set; }

        /// <summary>
        /// Gets or sets the charts.
        /// </summary>
        /// <value>The charts.</value>
        [JsonProperty("Charts")]
        public int Charts { get; set; }

        /// <summary>
        /// Gets or sets the trades.
        /// </summary>
        /// <value>The trades.</value>
        [JsonProperty("Trades")]
        public int Trades { get; set; }

        /// <summary>
        /// Gets or sets the orderbook.
        /// </summary>
        /// <value>The orderbook.</value>
        [JsonProperty("Orderbook")]
        public int Orderbook { get; set; }

        /// <summary>
        /// Gets or sets the forum.
        /// </summary>
        /// <value>The forum.</value>
        [JsonProperty("Forum")]
        public int Forum { get; set; }

        /// <summary>
        /// Gets or sets the influence.
        /// </summary>
        /// <value>The influence.</value>
        [JsonProperty("Influence")]
        public int Influence { get; set; }
    }
}
