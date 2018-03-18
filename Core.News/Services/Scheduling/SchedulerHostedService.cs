// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-18-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-18-2018
// ***********************************************************************
// <copyright file="SchedulerHostedService.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Core.News.Services
{
    /// <summary>
    /// Class SchedulerHostedService.
    /// </summary>
    /// <seealso cref="Core.News.Services.HostedService" />
    public class SchedulerHostedService : HostedService
    {
        /// <summary>
        /// Occurs when [unobserved task exception].
        /// </summary>
        public event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

        /// <summary>
        /// The scheduled tasks
        /// </summary>
        private readonly List<SchedulerTaskWrapper> _scheduledTasks = new List<SchedulerTaskWrapper>();
        /// <summary>
        /// The log factory
        /// </summary>
        private readonly ILoggerFactory logFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerHostedService" /> class.
        /// </summary>
        /// <param name="scheduledTasks">The scheduled tasks.</param>
        /// <param name="logFactory">The log factory.</param>
        /// <exception cref="ArgumentNullException">serviceScopeFactory</exception>
        public SchedulerHostedService(IEnumerable<IScheduledTask> scheduledTasks, ILoggerFactory logFactory)
        {            
            this.logFactory = logFactory;
            var referenceTime = DateTime.UtcNow;

            var log = logFactory.CreateLogger<SchedulerHostedService>();
            log.LogInformation("Scheduler Hosted Service is running.");

            foreach (var scheduledTask in scheduledTasks)
            {
                _scheduledTasks.Add(new SchedulerTaskWrapper
                {
                    Schedule = CrontabSchedule.Parse(scheduledTask.Schedule),
                    Task = scheduledTask,
                    NextRunTime = referenceTime
                });
            }            
        }

        /// <summary>
        /// execute as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ExecuteOnceAsync(cancellationToken);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        /// <summary>
        /// execute once as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
        {
            var taskFactory = new TaskFactory(TaskScheduler.Current);
            var referenceTime = DateTime.UtcNow;

            var tasksThatShouldRun = _scheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();

            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();

                await taskFactory.StartNew(
                    async () =>
                    {
                        try
                        {
                            await taskThatShouldRun.Task.ExecuteAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            var args = new UnobservedTaskExceptionEventArgs(
                                ex as AggregateException ?? new AggregateException(ex));

                            UnobservedTaskException?.Invoke(this, args);

                            if (!args.Observed)
                            {
                                throw;
                            }
                        }
                    },
                    cancellationToken);
            }
        }

        /// <summary>
        /// Class SchedulerTaskWrapper.
        /// </summary>
        private class SchedulerTaskWrapper
        {
            /// <summary>
            /// Gets or sets the schedule.
            /// </summary>
            /// <value>The schedule.</value>
            public CrontabSchedule Schedule { get; set; }
            /// <summary>
            /// Gets or sets the task.
            /// </summary>
            /// <value>The task.</value>
            public IScheduledTask Task { get; set; }

            /// <summary>
            /// Gets or sets the last run time.
            /// </summary>
            /// <value>The last run time.</value>
            public DateTime LastRunTime { get; set; }
            /// <summary>
            /// Gets or sets the next run time.
            /// </summary>
            /// <value>The next run time.</value>
            public DateTime NextRunTime { get; set; }

            /// <summary>
            /// Increments this instance.
            /// </summary>
            public void Increment()
            {
                LastRunTime = NextRunTime;
                NextRunTime = Schedule.GetNextOccurrence(NextRunTime);
            }

            /// <summary>
            /// Shoulds the run.
            /// </summary>
            /// <param name="currentTime">The current time.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            public bool ShouldRun(DateTime currentTime)
            {
                return NextRunTime < currentTime && LastRunTime != NextRunTime;
            }
        }
    }
}
