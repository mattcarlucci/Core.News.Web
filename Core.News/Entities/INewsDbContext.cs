// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-18-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-18-2018
// ***********************************************************************
// <copyright file="INewsDbContext.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace News.Core.SqlServer.Models
{
    /// <summary>
    /// Interface INewsDbContext
    /// </summary>
    public interface INewsDbContext
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Gets or sets the item contents.
        /// </summary>
        /// <value>The item contents.</value>
        DbSet<ItemContent> ItemContents { get; set; }
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        DbSet<Item> Items { get; set; }
    }
}