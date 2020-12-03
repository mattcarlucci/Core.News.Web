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
using AutoMapper;
using Core.News.Entities;
using Crypto.Compare.Extensions;
using Crypto.Compare.Models;
using Crypto.Compare.ViewModels;
using System;
using System.Net.Mail;
using Core.News.Mail;
using System.Linq;
//using Microsoft.AspNetCore.DataProtection;

namespace Core.News
{
    /// <summary>
    /// Class AutoMapperConfig.
    /// </summary>
    public static class AutoMapperConfig
    {
        static IMapper mapper;

        public static IMapper Instance => mapper;
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(
            cfg =>
               {
                   cfg.CreateMap<Publication, ItemContent>().
                   ForMember(dst => dst.CreatedDate, opt => opt.MapFrom(src => src.publishedOn.FromUnixTime())).
                   ForMember(dst => dst.Content, opt => opt.MapFrom(src => src.Body)).
                   ForMember(dst => dst.SourceUrl, opt => opt.MapFrom(src => src.Url)).
                   ForMember(dst => dst.CreatedBy, opt => opt.MapFrom(src => src.Source.Name)).
                   ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title)).
                   ForMember(dst => dst.ModifiedDate, opt => opt.MapFrom(src => src.publishedOn.FromUnixTime())).
                   ForMember(dst => dst.SortDescription, opt => opt.MapFrom(src => src.Body)).
                   ForMember(dst => dst.SmallImage, opt => opt.MapFrom(src => src.ImageUrl)).
                   ForMember(dst => dst.MediumImage, opt => opt.MapFrom(src => src.ImageUrl)).
                   ForMember(dst => dst.BigImage, opt => opt.MapFrom(src => src.ImageUrl)).
                   ForMember(dst => dst.NumOfView, opt => opt.MapFrom(src => 1)).
                   ForMember(dst => dst.RawData, opt => opt.MapFrom(src => src.UrlData)).
                   ForMember(dst => dst.Id, opt => opt.Ignore());

                   cfg.CreateMap<Provider, Category>().
                   ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name)).
                   ForMember(dst => dst.CreatedDate, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime())).
                   ForMember(dst => dst.CreatedBy, opt => opt.MapFrom(src => src.Name)).
                   ForMember(dst => dst.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()));

                   cfg.CreateMap<Item, Item>().
                   ForMember(dst => dst.CategoryId, opt => opt.MapFrom(src => src.Category.Id)).
                   ForMember(dst => dst.ItemContentId, opt => opt.MapFrom(src => src.ItemContent.Id)).
                   ForMember(dst => dst.CreatedBy, opt => opt.MapFrom(src => src.ItemContent.CreatedBy)).
                   ForMember(dst => dst.CreatedDate, opt => opt.MapFrom(src => src.ItemContent.CreatedDate)).
                   ForMember(dst => dst.ModifiedDate, opt => opt.MapFrom(src => src.ItemContent.ModifiedDate));

                   cfg.CreateMap<Publication, StoryViewModel>().
                   ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.Source.ImageUrl)).
                   ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Source.Name)).
                   ForMember(dst => dst.Elapsed, opt => opt.MapFrom(src => src.GetElapsedTime())).
                   ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title)).
                   ForMember(dst => dst.Body, opt => opt.MapFrom(src => src.Body)).
                   ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Url));

                   cfg.CreateMap<ItemContent, StoryViewModel>().
                   ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.SmallImage)).
                   ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.CreatedBy)).
                   ForMember(dst => dst.Elapsed, opt => opt.MapFrom(src => src.GetElapsedTime())).
                   ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title)).
                   ForMember(dst => dst.Body, opt => opt.MapFrom(src => src.Content)).
                   ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.SourceUrl));

                   cfg.CreateMap<SmtpConfiguration, SmtpClient>().
                   ForMember(dst => dst.Host, opt => opt.MapFrom(src => src.Host)).
                   ForMember(dst => dst.Port, opt => opt.MapFrom(src => src.Port)).
                   ForMember(dst => dst.Credentials, opt => opt.MapFrom(src => src.Credentials)).
                   ForMember(dst => dst.EnableSsl, opt => opt.MapFrom(src => src.EnableSsl)).
                   ForMember(dst => dst.UseDefaultCredentials, opt => opt.MapFrom(src => src.UseDefaultCredentials));

                   cfg.CreateMap<EmailConfiguration, MailMessage>().
                   ForMember(dst => dst.From, opt => opt.MapFrom(src => src.From.MailAddress)).
                     ForMember(dst => dst.IsBodyHtml, opt => opt.MapFrom(src => true)).
                   ForMember(dst => dst.SubjectEncoding, opt => opt.MapFrom(src => System.Text.Encoding.UTF8));


                   cfg.CreateMap<UserConfiguration, MailMessage>().
                   ForMember(dst => dst.To, opt => opt.MapFrom(src => src.To.Where(w => w.Enabled))).
                   ForMember(dst => dst.CC, opt => opt.MapFrom(src => src.Cc.Where(w => w.Enabled))).
                   ForMember(dst => dst.Bcc, opt => opt.MapFrom(src => src.Bcc.Where(w => w.Enabled))).
                   ForMember(dst => dst.IsBodyHtml, opt => opt.MapFrom(src => true)).
                   ForMember(dst => dst.SubjectEncoding, opt => opt.MapFrom(src => System.Text.Encoding.UTF8));
               });
            mapper = new Mapper(config);
        }
    }
}
