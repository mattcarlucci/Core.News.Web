// ***********************************************************************
// Assembly         : Crypto.Compare.Tests
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="UnitTest1.cs" company="Crypto.Compare.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Crypto.Compare.Proxies;
using System;
using Xunit;

namespace Crypto.Compare.Tests
{
    /// <summary>
    /// Class UnitTest1.
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Verifies the connection.
        /// </summary>
        [Fact]
        public void VerifyConnection()
        {
            WebApiClient client = new WebApiClient(true, DateTime.Now.AddDays(1));
            client.RequestLatestNews();
            Assert.Equal(0, client.StoryCount);
        }
    }
}
