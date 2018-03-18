using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.News.Web.Entities
{
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
        [StringLength(200)]
        public string SmallImage { get; set; }

        /// <summary>
        /// Gets or sets the medium image.
        /// </summary>
        /// <value>The medium image.</value>
        [Required]
        [StringLength(200)]
        public string MediumImage { get; set; }

        /// <summary>
        /// Gets or sets the big image.
        /// </summary>
        /// <value>The big image.</value>
        [Required]
        [StringLength(200)]
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
    }
}