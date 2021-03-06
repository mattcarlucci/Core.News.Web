﻿// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="Item.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.News.Entities
{
    /// <summary>
    /// Class Item.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="content">The content.</param>
        public Item(Category category, ItemContent content)
        {
            this.Category = category;
            this.ItemContent = content;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        public Item() { }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the item content identifier.
        /// </summary>
        /// <value>The item content identifier.</value>
        public int ItemContentId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The modified date.</value>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the content of the item.
        /// </summary>
        /// <value>The content of the item.</value>
        public virtual ItemContent ItemContent { get; set; }
        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <returns>Item.</returns>
        public Item Reset()
        {
            this.Category = null;
            this.ItemContent = null;
            return this;
        }
    }
}