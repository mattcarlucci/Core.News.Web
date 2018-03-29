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
using Core.News.Services;
using Core.News.Entities;
using Crypto.Compare.Extensions;
using Crypto.Compare.Models;
using Crypto.Compare.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Core.News.Mail;
using System.Net;
using System.Linq;
using Serilog;
using Core.News.Console.Scheduling;

namespace Core.News
{
    /// <summary>
    /// Class Bootstrap.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void InitializeAsync()
        {            
            IServiceCollection services = new ServiceCollection();         

            Startup startup = new Startup();
            startup.ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Trace);
            loggerFactory.AddFile("Logs/Core.News.Console-{Date}.log");

            Log.Logger = new LoggerConfiguration()
         //    WriteTo.Console().
            .WriteTo.RollingFile("Logs/trace.log", Serilog.Events.LogEventLevel.Verbose).CreateLogger();
          

            AutoMapperConfig.RegisterMappings();

            StartServices(serviceProvider);         
        }

        /// <summary>
        /// Starts the services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        private static void StartServices(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger<Program>>();
            var svc = serviceProvider.GetService<IEmailSchedulingService>();               
          
            logger.LogDebug(PerfJob.GetProcessInfo());

            try
            {
                IWebClientService webClient = serviceProvider.GetService<IWebClientService>();
                var task = webClient.StartAsync(new System.Threading.CancellationToken());

                svc.CreateJobs();

                task.Wait();               
            }
            catch(Exception ex)
            {
               svc.Shutdown();
               logger.LogCritical(ex, ex.Message);
               logger.LogDebug(PerfJob.GetProcessInfo());
               System.Threading.Thread.Sleep(-1);
            }
        }
    }


    /// <summary>
    /// Class AutoMapperConfig.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {           
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Publication, ItemContent>().
                ForMember(dst => dst.CreatedDate, opt => opt.MapFrom(src => src.publishedOn.FromUnixTime())).
                ForMember(dst => dst.Content, opt => opt.MapFrom(src => src.Body)).
                ForMember(dst => dst.SourceUrl, opt => opt.MapFrom(src =>  src.Url)).
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
        }
    }

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
        public static SmtpClient MapSmtpClient(SmtpConfiguration config)
        {
            return AutoMapper.Mapper.Map<SmtpClient>(config);
        }
        /// <summary>
        /// Maps the mail address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>MailAddress.</returns>
        public static MailAddress MapMailAddress(EmailAddress address)
        {
            return AutoMapper.Mapper.Map<MailAddress>(address);
        }
        /// <summary>
        /// Maps to address.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <returns>MailAddressCollection.</returns>
        public static MailAddressCollection MapToAddress(List<EmailAddress> addresses)
        { 
            return AutoMapper.Mapper.Map<MailAddressCollection>(addresses);
        }

        /// <summary>
        /// Maps the address.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="bcc">The BCC.</param>
        /// <returns>System.ValueTuple.MailAddressCollection.MailAddressCollection.MailAddressCollection.</returns>
        public static (MailAddressCollection To, MailAddressCollection Cc, MailAddressCollection Bcc) 
            MapAddresses(MailMessage message, List<EmailAddress> to, List<EmailAddress> cc, List<EmailAddress> bcc)
        {
            // message.To = MapToAddress(to);
            var _to = MapToAddress(to);
            var _cc = MapToAddress(cc);
            var _bc = MapToAddress(bcc);
            return (_to, _cc, _bc);

        }
        /// <summary>
        /// Maps the mail configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>MailMessage.</returns>
        public static MailMessage MapMailConfiguration(IEmailConfiguration config, Func<EmailAddress, bool> predicate)
        {
            var mail = AutoMapper.Mapper.Map<MailMessage>(config);
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
        public static MailMessage MapRecipients(UserConfiguration config, Func<EmailAddress, bool> predicate)
        {
            var mail = AutoMapper.Mapper.Map<MailMessage>(config);
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
            return AutoMapper.Mapper.Map<StoryViewModel>(publication);
        }

        /// <summary>
        /// Maps the story view.
        /// </summary>
        /// <param name="publications">The publications.</param>
        /// <returns>List&lt;StoryViewModel&gt;.</returns>
        public static StoryViewModels StoryView(List<Publication> publications)
        {
            IMapper mapper = AutoMapper.Mapper.Instance;
            var stories = mapper.Map<List<StoryViewModel>>(publications);

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
            IMapper mapper = AutoMapper.Mapper.Instance;
            var stories = mapper.Map<List<StoryViewModel>>(publications);

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
            IMapper mapper = AutoMapper.Mapper.Instance;
            return mapper.Map<List<ItemContent>>(stories);
        }
        /// <summary>
        /// Maps the story.
        /// </summary>
        /// <param name="story">The story.</param>
        /// <returns>ItemContent.</returns>
        public static ItemContent Story(Publication story)
        {
            return AutoMapper.Mapper.Map<ItemContent>(story);
        }

        /// <summary>
        /// Maps the provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public static Category Provider(Provider provider)
        {
            return AutoMapper.Mapper.Map<Category>(provider);
        }

        /// <summary>
        /// Maps the providers.
        /// </summary>
        /// <param name="providers">The providers.</param>
        /// <returns>List&lt;Category&gt;.</returns>
        public static List<Category> Providers(List<Provider> providers)
        {
            IMapper mapper = AutoMapper.Mapper.Instance;
            return mapper.Map<List<Category>>(providers);
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
            return AutoMapper.Mapper.Map<Item>(item).Reset();
        }
    }
}
