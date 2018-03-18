// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-18-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-18-2018
// ***********************************************************************
// <copyright file="IScheduledTask.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;


namespace Core.News.Services
{
    /// <summary>
    /// Interface IScheduledTask
    /// </summary>
    public interface IScheduledTask
    {
        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <value>The schedule.</value>
        string Schedule { get; }
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
