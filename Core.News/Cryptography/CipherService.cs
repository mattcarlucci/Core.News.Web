// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 04-06-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 04-06-2018
// ***********************************************************************
// <copyright file="CipherService.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.News.Cryptography
{
    /// <summary>
    /// Class CipherService.
    /// </summary>
    /// <seealso cref="Core.News.Cryptography.ICipherService" />
    public class CipherService : ICipherService
    {
        /// <summary>
        /// The data protection provider
        /// </summary>
        private readonly IDataProtectionProvider dataProtectionProvider;

        /// <summary>
        /// The key provider
        /// </summary>
        private readonly ICipherKeyProvider keyProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CipherService"/> class.
        /// </summary>
        /// <param name="dataProtectionProvider">The data protection provider.</param>
        /// <param name="keyProvider">The key provider.</param>
        public CipherService(IDataProtectionProvider dataProtectionProvider, ICipherKeyProvider keyProvider)
        {
            this.dataProtectionProvider = dataProtectionProvider;
            this.keyProvider = keyProvider;
        }

        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public string Encrypt(string input)
        {
            var protector = dataProtectionProvider.CreateProtector(keyProvider.Key);
            return protector.Protect(input);
        }

        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>System.String.</returns>
        public string Decrypt(string cipherText)
        {
            var protector = dataProtectionProvider.CreateProtector(keyProvider.Key);
            return protector.Unprotect(cipherText);
        }
    }
}
