// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="INewsRepository.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.News.Entities;
using System;
using System.Collections.Generic;

namespace Core.News.Repositories
{
    /// <summary>
    /// Interface INewsRepository
    /// </summary>
    public interface INewsRepository
    {
        /// <summary>
        /// Adds the update category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>Category.</returns>
        Category AddUpdateCategory(Category category);
        /// <summary>
        /// Adds the update item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Item.</returns>
        Item AddUpdateItem(Item item);
        /// <summary>
        /// Adds the update story.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="content">The content.</param>
        /// <returns>ItemContent.</returns>
        ItemContent AddUpdateStory(Category category, ItemContent content);
        /// <summary>
        /// Gets the last content date.
        /// </summary>
        /// <returns>DateTime.</returns>
        DateTime GetLastContentDate();
        ///// <summary>
        ///// Gets the schedules.
        ///// </summary>
        ///// <param name="schedule">The schedule.</param>
        ///// <returns>ScheduleViewModel.</returns>
        //ScheduleViewModel GetSchedules(string schedule);
        //string[] GetSchedules();
    }
}
