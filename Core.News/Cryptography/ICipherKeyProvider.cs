// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 04-06-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 04-06-2018
// ***********************************************************************
// <copyright file="ICipherKeyProvider.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Core.News.Cryptography
{
    /// <summary>
    /// Interface ICipherKeyProvider
    /// </summary>
    public interface ICipherKeyProvider
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        string Key { get; }
    }
}
