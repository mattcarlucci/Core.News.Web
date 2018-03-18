// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="CoreNewsDbContext.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Core.News.Web.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Core.News
{
    /// <summary>
    /// Class NewsDbContext.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class NewsDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the item contents.
        /// </summary>
        /// <value>The item contents.</value>
        public virtual DbSet<ItemContent> ItemContents { get; set; }
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public virtual DbSet<Item> Items { get; set; }
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsDbContext"/> class.
        /// </summary>
        public NewsDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cn = Startup.Configuration["SqlServer"];
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SqlNews"].ConnectionString);
        }
    }

}
