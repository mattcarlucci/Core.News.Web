// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-21-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-21-2018
// ***********************************************************************
// <copyright file="CRONs.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using CronExpressionDescriptor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.News
{
    /// <summary>
    /// Class CronParser.
    /// </summary>
    public class CronExpression 
    {
        /// <summary>
        /// The cron
        /// </summary>
        private Quartz.CronExpression cron;
        /// <summary>
        /// The keys
        /// </summary>
        static Dictionary<string, string> Keys = CronExprs.GetPairs();

        /// <summary>
        /// Initializes a new instance of the <see cref="CronExpression"/> class.
        /// </summary>
        /// <param name="cronExpression">The cron expression.</param>
        public CronExpression(string cronExpression) 
        {
            if (Keys.ContainsKey(cronExpression))
                cronExpression = Keys[cronExpression];    
            
            cron = new Quartz.CronExpression(cronExpression);           
        }

        /// <summary>
        /// Gets the verbose.
        /// </summary>
        /// <param name="cronExpression">The cron expression.</param>
        /// <returns>System.String.</returns>
        public string GetDisplay(string cronExpression)
        {
            return ExpressionDescriptor.GetDescription(cronExpression);
                }

        /// <summary>
        /// Gets the next valid time after.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>System.DateTimeOffset.</returns>
        public DateTimeOffset GetNextValidTimeAfter(DateTimeOffset dt)
        {
            return cron.GetNextValidTimeAfter(dt).GetValueOrDefault();
        }

        /// <summary>
        /// Gets the array of times.
        /// </summary>
        /// <param name="cronExpression">The cron expression.</param>
        /// <returns>IEnumerable&lt;System.Nullable&lt;DateTimeOffset&gt;&gt;.</returns>
        /// <exception cref="ArgumentException">cronExpression</exception>
        public static IEnumerable<DateTimeOffset?> GetArrayOfTimes(string cronExpression)
        {
            if (string.IsNullOrEmpty(cronExpression)) throw new ArgumentException("cronExpression");
            var result = new List<DateTimeOffset?>();
            var jobSchedules = new List<DateTimeOffset>();
            var quartzHelper = new Quartz.CronExpression(cronExpression);
            var dt = DateTimeOffset.Now;
            var numberOfDays = 0;
            var numberOfSchedules = 30;
            var restrictToDays = 20;

            while (numberOfDays <= restrictToDays)
            {
                var nextScheduledJob = quartzHelper.GetNextValidTimeAfter(dt);

                if (nextScheduledJob != null)
                {
                    dt = nextScheduledJob.Value;
                    if (result.Count != 0)
                    {
                        var lastDate = result.Last();
                        var diff = dt.Day - lastDate.Value.Day;
                        if (diff >= 1)
                        {
                            numberOfDays++;
                        }
                    }
                    result.Add(nextScheduledJob.GetValueOrDefault().ToLocalTime());
                    if (result.Count() == numberOfSchedules)
                    {
                        break;
                    }
                }
            }
            return result;
        }

    }
}
