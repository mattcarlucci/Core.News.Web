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

namespace News.Core.SqlServer
{
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
    }
}
