// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-16-2018
// ***********************************************************************
// <copyright file="ItemContent.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.News.Entities
{
    /// <summary>
    /// Class ItemContent.
    /// </summary>
    public class ItemContent
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the sort description.
        /// </summary>
        /// <value>The sort description.</value>
        [Required]
        public string SortDescription { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the raw data.
        /// </summary>
        /// <value>The raw data.</value>
        public string RawData { get; set; }

        /// <summary>
        /// Gets or sets the small image.
        /// </summary>
        /// <value>The small image.</value>
        [Required]
        [StringLength(300)]
        public string SmallImage { get; set; }

        /// <summary>
        /// Gets or sets the medium image.
        /// </summary>
        /// <value>The medium image.</value>
        [Required]
        [StringLength(300)]
        public string MediumImage { get; set; }

        /// <summary>
        /// Gets or sets the big image.
        /// </summary>
        /// <value>The big image.</value>
        [Required]
        [StringLength(300)]
        public string BigImage { get; set; }

        /// <summary>
        /// Gets or sets the number of view.
        /// </summary>
        /// <value>The number of view.</value>
        public long? NumOfView { get; set; }

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
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public virtual List<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the source URL.
        /// </summary>
        /// <value>The source URL.</value>
        [StringLength(300)]
        public virtual string SourceUrl { get; set; }

        /// <summary>
        /// Gets the elapsed time.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetElapsedTime()
        {
            TimeSpan span = new TimeSpan(
                DateTime.Now.ToUniversalTime().Ticks -
                CreatedDate.Ticks);

            int elapse = (int)span.TotalHours == 0
                ? (int)span.TotalMinutes : (int)span.TotalHours;

            string sp = (int)span.TotalHours == 0
                ? "minutes" : "hours";

            return string.Format("{0} {1} ago", elapse, sp);
        }
    }
}