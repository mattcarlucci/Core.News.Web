// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 04-06-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 04-06-2018
// ***********************************************************************
// <copyright file="ICipherService.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Core.News.Cryptography
{
    /// <summary>
    /// Interface ICipherService
    /// </summary>
    public interface ICipherService
    {
        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>System.String.</returns>
        string Decrypt(string cipherText);
        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        string Encrypt(string input);
    }
}