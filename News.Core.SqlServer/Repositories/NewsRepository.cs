using Core.News;
using Core.News.Web.Entities;
using News.Core.SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace News.Core.SqlServer
{
    public class NewsRepository : INewsRepository
    {
        INewsDbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public NewsRepository(NewsDbContext db)
        {
            this.db = db;
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
            using (NewsDbContext db = new NewsDbContext())
            {
                var _item = db.Items.SingleOrDefault(s =>
                s.CategoryId == item.CategoryId && s.ItemContentId == item.ItemContentId);

                if (_item != null) return _item;
                db.Items.Add(item);
                db.SaveChanges();
                return item;
            }
        }
        /// <summary>
        /// Adds the update category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>Category.</returns>

        public Category AddUpdateCategory(Category category)
        {
            using (NewsDbContext db = new NewsDbContext())
            {
                var _category = db.Categories.SingleOrDefault(s => s.Name == category.Name);
                if (_category != null) return _category;

                db.Categories.Add(category);
                db.SaveChanges();
                return category;
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
            using (NewsDbContext db = new NewsDbContext())
            {
                var _content = db.ItemContents.SingleOrDefault(s =>
            s.Title == content.Title && s.CreatedBy == content.CreatedBy);

                if (_content != null) return _content;
                db.ItemContents.Add(content);

                db.SaveChanges();
                return content;
            }
        }

    }
}
