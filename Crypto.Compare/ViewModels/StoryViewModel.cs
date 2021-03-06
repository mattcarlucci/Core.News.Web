﻿// ***********************************************************************
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Compare.ViewModels
{

    /// <summary>
    /// Class StoryViewModel.
    /// </summary>
    public class StoryViewModel
    {
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the elapsed.
        /// </summary>
        /// <value>The elapsed.</value>
        public string Elapsed { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            // var anchor = "<a href=\"{0}/\"> Read More </a>";

            string html = $"<img src=\"{ImageUrl}\" alt=\"\" height=\"16\" width=\"16\"/>";
            html += $"<font color=\"#ebad02\"> {Name}</font> - <font color=\"#dedbd5\">{Elapsed}</font>";
            html += "<br></br> <br></br>";
            html += $"<a href =\"{Url}\"><font color=\"green\"><b>{Title}</b></font></a>";
            html += "<br></br> <br></br>";
            html += $"{Body}";
            if (!Body.Contains("> Read More"))
            html += $"<a href =\"{Url}\">Read More</a>";
            html += "<br></br> <br></br>";
            html += "<hr width =\"100%\"/>";
            return html;

        }
    }
}
