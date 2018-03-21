// ***********************************************************************
// Assembly         : News.Core.SqlServer
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="NewsDbContext.cs" company="News.Core.SqlServer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News;
using Core.News.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Linq;

namespace News.Core.SqlServer.Models
{
    /// <summary>
    /// Class NewsDbContext.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class NewsDbContext : DbContext, INewsDbContext
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
               
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
           : base(options)
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
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            //optionsBuilder.UseSqlServer(configuration.Connections.
            //    Single(s => s.Key == configuration.Connection).Value);
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        /// <remarks>This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.</remarks>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        /// <summary>
        /// Releases the allocated resources for this context.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}