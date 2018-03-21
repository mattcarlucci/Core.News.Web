// ***********************************************************************
// Assembly         : Crypto.Compare.Tests
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-20-2018
// ***********************************************************************
// <copyright file="UnitTest1.cs" company="Crypto.Compare.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News;
using Crypto.Compare.Models;
using Crypto.Compare.Models.Historical;
using Crypto.Compare.Models.SocialStats;
using Crypto.Compare.Proxies;
using Crypto.Compare.Repositories;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Crypto.Compare.Tests
{
    /// <summary>
    /// Class UnitTest1.
    /// </summary>
    public class UnitTest1
    {
        //TODO: Make this a real test.
        /// <summary>
        /// The repo
        /// </summary>
        static CryptoRepository repo = new CryptoRepository();
        /// <summary>
        /// The coins
        /// </summary>
        static List<CryptoCoin> coins = repo.GetCryptoCoins();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTest1"/> class.
        /// </summary>
        public UnitTest1()
        {
           
        }

        /// <summary>
        /// Verifies the connection.
        /// </summary>
        [Fact]
        public void VerifyConnection()
        {
            NewsApiClient client = new NewsApiClient(true, DateTime.Now.AddDays(1));
            client.RequestLatestNews();
            Assert.Equal(0, client.StoryCount);
        }
        /// <summary>
        /// Verifies the social stats.
        /// </summary>
        [Fact]
        public void VerifySocialStats()
        {
            repo.GetSocialStats(coins);
        }
        /// <summary>
        /// Verifies the daily bars.
        /// </summary>
        [Fact]
        public void VerifyDailyBars()
        {
            (var day_good, var day_bad) = repo.GetHistorical<DailyBars>(coins, "histoday");

        }
        /// <summary>
        /// Verifies the hour bars.
        /// </summary>
        [Fact]
        public void VerifyHourBars()
        {
            (var hour_good, var hour_bad) = repo.GetHistorical<HourBars>(coins, "histohour");

        }
        /// <summary>
        /// Verifies the minute bars.
        /// </summary>
        [Fact]
        public void VerifyMinuteBars()
        {
            (var minute_good, var minute_bad) = repo.GetHistorical<MinuteBars>(coins, "histominute");

        }
    }
}
