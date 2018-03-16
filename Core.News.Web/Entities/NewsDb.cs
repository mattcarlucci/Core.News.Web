using Microsoft.EntityFrameworkCore;

namespace Core.News.Web.Entities
{
    public class NewsDb : DbContext
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public virtual DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public virtual DbSet<Item> Items { get; set; }
        /// <summary>
        /// Gets or sets the item contents.
        /// </summary>
        /// <value>The item contents.</value>
        public virtual DbSet<ItemContent> ItemContents { get; set; }

        public NewsDb(DbContextOptions<NewsDb> options)
           : base(options)
        { }
    }
}