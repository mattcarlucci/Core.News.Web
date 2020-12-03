// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="Bootstrap.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News.Entities;
using Crypto.Compare.Models;
using Crypto.Compare.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Core.News.Mail;
using System.Linq;
//using Microsoft.AspNetCore.DataProtection;

namespace Core.News
{
    /// <summary>
    /// Class Map.
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// Maps the SMTP client.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>SmtpClient.</returns>
        public static SmtpClient SmtpClient(SmtpConfiguration config)
        {
            return AppService.Mapper.Map<SmtpClient>(config);
        }
        /// <summary>
        /// Maps the mail address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>MailAddress.</returns>
        public static MailAddress MailAddress(EmailAddress address)
        {
            return AppService.Mapper.Map<MailAddress>(address);
        }
        /// <summary>
        /// Maps to address.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <returns>MailAddressCollection.</returns>
        public static MailAddressCollection ToAddress(List<EmailAddress> addresses)
        {
            return AppService.Mapper.Map<MailAddressCollection>(addresses);
        }

        /// <summary>
        /// Maps the address.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="bcc">The BCC.</param>
        /// <returns>System.ValueTuple.MailAddressCollection.MailAddressCollection.MailAddressCollection.</returns>
        public static (MailAddressCollection To, MailAddressCollection Cc, MailAddressCollection Bcc)
            Addresses(MailMessage message, List<EmailAddress> to, List<EmailAddress> cc, List<EmailAddress> bcc)
        {
            // message.To = MapToAddress(to);
            var _to = ToAddress(to);
            var _cc = ToAddress(cc);
            var _bc = ToAddress(bcc);
            return (_to, _cc, _bc);

        }
        /// <summary>
        /// Maps the mail configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>MailMessage.</returns>
        public static MailMessage MailConfiguration(IEmailConfiguration config, Func<EmailAddress, bool> predicate)
        {
            var mail = AppService.Mapper.Map<MailMessage>(config);
            mail.To.AddRange(config.Users.To.Where(predicate));
            mail.CC.AddRange(config.Users.Cc.Where(predicate));
            mail.Bcc.AddRange(config.Users.Bcc.Where(predicate));

            return mail;
        }
        /// <summary>
        /// Maps the recipients.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="">The .</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>System.Net.Mail.MailMessage.</returns>
        public static MailMessage Recipients(UserConfiguration config, Func<EmailAddress, bool> predicate)
        {
            var mail = AppService.Mapper.Map<MailMessage>(config);
            mail.To.AddRange(config.To.Where(predicate));
            mail.CC.AddRange(config.To.Where(predicate));
            mail.Bcc.AddRange(config.To.Where(predicate));
            return mail;
        }
        /// <summary>
        /// Maps the story view.
        /// </summary>
        /// <param name="publication">The publication.</param>
        /// <returns>StoryViewModel.</returns>
        public static StoryViewModel StoryView(Publication publication)
        {
            return AppService.Mapper.Map<StoryViewModel>(publication);
        }

        /// <summary>
        /// Maps the story view.
        /// </summary>
        /// <param name="publications">The publications.</param>
        /// <returns>List&lt;StoryViewModel&gt;.</returns>
        public static StoryViewModels StoryView(List<Publication> publications)
        {
            var stories = AppService.Mapper.Map<List<StoryViewModel>>(publications);

            StoryViewModels model = new StoryViewModels(stories);
            return model;
        }
        /// <summary>
        /// Stories the view.
        /// </summary>
        /// <param name="publications">The publications.</param>
        /// <returns>StoryViewModels.</returns>
        public static StoryViewModels StoryView(List<ItemContent> publications)
        {
            var stories = AppService.Mapper.Map<List<StoryViewModel>>(publications);

            StoryViewModels model = new StoryViewModels(stories);
            return model;
        }
        /// <summary>
        /// Maps the stories.
        /// </summary>
        /// <param name="stories">The stories.</param>
        /// <returns>List&lt;ItemContent&gt;.</returns>
        public static List<ItemContent> Stories(List<Publication> stories)
        {
            return AppService.Mapper.Map<List<ItemContent>>(stories);
        }
        /// <summary>
        /// Maps the story.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <returns>ItemContent.</returns>
        public static ItemContent Story(Publication story)
        {
            return AppService.Mapper.Map<ItemContent>(story);
        }

        /// <summary>
        /// Maps the provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public static Category Provider(Provider provider)
        {
            return AppService.Mapper.Map<Category>(provider);
        }

        /// <summary>
        /// Maps the providers.
        /// </summary>
        /// <param name="providers">The providers.</param>
        /// <returns>List&lt;Category&gt;.</returns>
        public static List<Category> Providers(List<Provider> providers)
        {
            return AppService.Mapper.Map<List<Category>>(providers);
        }

        /// <summary>
        /// Maps the item.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="content">The content.</param>
        /// <returns>Item.</returns>
        public static Item Item(Category category, ItemContent content)
        {
            var item = new Item(category, content);
            return AppService.Mapper.Map<Item>(item).Reset();
        }
    }
}
