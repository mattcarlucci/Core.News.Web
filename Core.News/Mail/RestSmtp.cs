// ***********************************************************************
// Assembly         : Core.News
// Author           : Matt.Carlucci
// Created          : 11-30-2020
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 11-30-2020
// ***********************************************************************
// <copyright file="RestSmtp.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Core.News.Mail;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Scheduling namespace.
/// </summary>
namespace Core.News.Console.Scheduling
{
    /// <summary>
    /// Class RestEmail.
    /// </summary>
    public static class RestEmail
    {
        #region Email Methods
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <param name="smtp">The SMTP.</param>
        /// <returns>RestRequestAsyncHandle.</returns>
        public static IRestResponse SendEmail(MailMessage mail, RestSmtpConfiguration smtp)
        {
            var toAddresses = mail.To.Select(s => s.Address);
            var fromAddress = $"{mail.From.DisplayName} <{mail.From.Address}>";
            var subject = mail.Subject;
            var body = mail.Body;          

            return SendEmail(smtp.HostUrl, smtp.Domain, smtp.ApiKey,  
                toAddresses, fromAddress, subject, body, null);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="toAddresses">To addresses.</param>
        /// <param name="fromAddress">From address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="attachments">The attachments.</param>
        /// <returns>RestRequestAsyncHandle.</returns>
        public static IRestResponse SendEmail(
            string host, string domain, string apiKey, 
            IEnumerable<string> toAddresses, string fromAddress, string subject, string body, string[] attachments = null)
        {
            RestClient client = new RestClient
            {
                BaseUrl = new Uri(host),
                Authenticator =
                new HttpBasicAuthenticator("api", apiKey)
            };
           
            IRestRequest request = new RestRequest();
            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", fromAddress);

            foreach (var address in toAddresses)
                request.AddParameter("to", address);

            request.AddParameter("subject", subject);
            request.AddParameter("html", body);

            if (attachments != null)
                foreach (var attachment in attachments)
                    request.AddFile("attachment", attachment);
           
            request.Method = Method.POST;
            return client.Execute(request);            
        }

        private static void EmailCallback(IRestResponse arg1, RestRequestAsyncHandle arg2)
        {
        }
        #endregion
    }
}
