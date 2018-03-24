// ***********************************************************************
// Assembly         : Crypto.News
// Author           : mcarlucci
// Created          : 03-14-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-14-2018
// ***********************************************************************
// <copyright file="StoryViewModel.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace Crypto.Compare.ViewModels
{
    /// <summary>
    /// Class StoryViewModels.
    /// </summary>
    public class StoryViewModels 
    {
        /// <summary>
        /// Gets or sets the story count.
        /// </summary>
        /// <value>The story count.</value>
        public int StoryCount { get; set; }
        /// <summary>
        /// Gets or sets the stories.
        /// </summary>
        /// <value>The stories.</value>
        public virtual List<StoryViewModel> Stories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryViewModels"/> class.
        /// </summary>
        /// <param name="stories">The stories.</param>
        public StoryViewModels(List<StoryViewModel> stories)
        {
            Stories = stories;
            StoryCount = stories.Count();
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0} new {1}<br><br>", 
                StoryCount, StoryCount > 1 ? "stories" : "story");
        }
        /// <summary>
        /// Mails the message.
        /// </summary>
        /// <returns>System.String.</returns>
        public string MailMessage()
        {
            //string token = "<hr width =\"100%\"><br>";
            return this + string.Join("<br>", Stories) +  GetImageUrls();
        }
        public string GetImageUrls()
        {
            string html = "<img src=\"{0}\" alt=\"\" height=\"100\" width=\"100\">";            
            return string.Join(" ", Stories.Select(s => string.Format(html, s.ImageUrl)).Distinct());
        }
    }
}
