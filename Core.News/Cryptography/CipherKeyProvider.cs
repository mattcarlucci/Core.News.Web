// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 04-06-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 04-06-2018
// ***********************************************************************
// <copyright file="CipherKeyProvider.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;

namespace Core.News.Cryptography
{
    /// <summary>
    /// Class CipherKeyProvider.
    /// </summary>
    /// <seealso cref="Core.News.Cryptography.ICipherKeyProvider" />
    public class CipherKeyProvider : ICipherKeyProvider
    {
        /// <summary>
        /// The key
        /// </summary>
        private static string key = null;
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get { return key ?? getKey(); } }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <returns>System.String.</returns>
        private string getKey()
        {
            key = File.ReadAllText(@".\keys\email.key");
            return key;
        }
    }
}
