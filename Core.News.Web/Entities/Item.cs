using System;
using System.ComponentModel.DataAnnotations;

namespace Core.News.Web.Entities
{
    public class Item
    {
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

    }
}