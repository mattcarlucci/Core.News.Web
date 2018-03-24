// ***********************************************************************
// Assembly         : News.Core.SqlServer
// Author           : mcarlucci
// Created          : 03-19-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-24-2018
// ***********************************************************************
// <copyright file="NewsRepository.cs" company="News.Core.SqlServer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News;
using Core.News.Entities;
using News.Core.SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Core.News.Repositories;


/// <summary>
/// The SqlServer namespace.
/// </summary>
namespace News.Core.SqlServer
{
    /// <summary>
    /// Class NewsRepository.
    /// </summary>
    /// <seealso cref="Core.News.Repositories.INewsRepository" />
    public class NewsRepository : INewsRepository
    {
        /// <summary>
        /// The database
        /// </summary>
        INewsDbContext db;

        /// <summary>
        /// The provider
        /// </summary>
        IServiceProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public NewsRepository(NewsDbContext db, ServiceProvider provider)
        {
            this.db = db;
            this.provider = provider;
            db.Database.Migrate();
        }
      
        /// <summary>
        /// Gets the last content date.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime GetLastContentDate()
        {
            if (db.ItemContents.Count() == 0) return DateTime.Now.AddDays(-2);
            return db.ItemContents.Max(m => m.CreatedDate);
        }

        /// <summary>
        /// Adds the update item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Item.</returns>

        public Item AddUpdateItem(Item item)
        {
            using (var scope = provider.CreateScope())
            {
                return scope.ServiceProvider.GetService<DbContext>()
                   .AddOrUpdate(item, w => w.CategoryId == item.CategoryId && w.ItemContentId == item.ItemContentId);              
            }        
        }

        /// <summary>
        /// Adds the update category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>Category.</returns>

        public Category AddUpdateCategory(Category category)
        {
            using (var scope = provider.CreateScope())
            {
                return scope.ServiceProvider.GetService<DbContext>()
                    .AddOrUpdate(category, w => w.Name == category.Name);             
            }            
        }
        /// <summary>
        /// Adds the update story.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="content">The content.</param>
        /// <returns>ItemContent.</returns>
        public ItemContent AddUpdateStory(Category category, ItemContent content)
        {
            using (var scope = provider.CreateScope())
            {
                return scope.ServiceProvider.GetService<DbContext>()
                   .AddOrUpdate(content, w => w.CreatedBy == content.CreatedBy && w.Title == content.Title);
            }
        }

        /// <summary>
        /// Gets the schedules.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns>System.ValueTuple.EmailConfiguration.List&lt;ItemContent&gt;.</returns>
        //public ScheduleViewModel GetSchedules(string schedule)
        //{
        //    var item = db.NewsSchedules.SingleOrDefault(s => s.Schedule == schedule);
        //    NewsConfiguration config = NewsConfiguration.Load();
         
        //    EmailConfiguration email = new EmailConfiguration
        //    {
        //        Smtp = config.EmailConfiguration.Smtp,
        //        Enabled = config.EmailConfiguration.Enabled,
        //        UseSsl = config.EmailConfiguration.UseSsl
        //    };

        //    email.User.To = config.EmailConfiguration.User.To.
        //        Where(w => w.Schedule == schedule && w.Enabled).ToList();

        //    email.User.Cc = config.EmailConfiguration.User.Cc.
        //        Where(w => w.Schedule == schedule && w.Enabled).ToList();

        //    email.User.Bcc = config.EmailConfiguration.User.Bcc.
        //        Where(w => w.Schedule == schedule && w.Enabled).ToList();
           
        //    var stories = db.ItemContents.
        //        Where(w => w.CreatedDate > item.NewsStamp).ToList();
          
        //    return new ScheduleViewModel(email, stories);
        //}

        /// <summary>
        /// Gets the schedules.
        /// </summary>
        /// <returns>System.String[].</returns>
        //public string[] GetSchedules()
        //{
        //    NewsConfiguration config = NewsConfiguration.Load();
        //    List<string> schedules = new List<string>();

        //    schedules.AddRange(config.EmailConfiguration.User.To.
        //        Where(w=> w.Enabled).Select(s => s.Schedule));

        //    schedules.AddRange(config.EmailConfiguration.User.Cc.
        //        Where(w => w.Enabled).Select(s => s.Schedule));

        //    schedules.AddRange(config.EmailConfiguration.User.Bcc.
        //        Where(w => w.Enabled).Select(s => s.Schedule));

        //    return schedules.Distinct().ToArray();

        //}
    }
}
