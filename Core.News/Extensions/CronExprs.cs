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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CronExpressionDescriptor;

namespace Core.News
{
    public class CronExprs
    {
        /// https://www.freeformatter.com/cron-expression-generator-quartz.html
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="exp">The exp.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public static IEnumerable<string> GetValues()
        {            
            return typeof(CronExprs).GetFields().
                Select(s => s.GetValue(s).ToString());
        }

        /// <summary>
        /// Gets the pairs.
        /// </summary>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;System.String, System.String&gt;&gt;.</returns>
        public static Dictionary<string,string> GetPairs()
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();

            typeof(CronExprs).GetFields().ToList().
                ForEach(item => pairs.Add(item.Name, item.GetValue(item).ToString()));

            return pairs;

        }
        /// <summary>
        /// Tests this instance.
        /// </summary>
        public static void Create()
        {
            const string file = ".\\Cron Schedule Helper.md";
            StringBuilder sb = new StringBuilder();
            foreach (var p in GetValues())
            {
                DateTime now = DateTime.Now.ToUniversalTime();
                try
                {
                    Quartz.CronExpression expr = new Quartz.CronExpression(p);
                    var value = expr.GetNextValidTimeAfter(now);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sb.AppendLine(p.PadRight(32) + "\t" +
                        ExpressionDescriptor.GetDescription(p));
                }

               
            }

            File.WriteAllText(file, sb.ToString());
        }
        /// <summary>
        /// every second
        /// </summary>
        public static string every_01_second = " * * * ? * * ";
        /// <summary>
        /// every ten seconds
        /// </summary>
        public static string every_10_seconds = "0/10 * * ? * * *";
        /// <summary>
        /// The every 30 seconds
        /// </summary>
        public static string every_30_seconds = "0/30 * * ? * * *";
        /// <summary>
        /// every minute
        /// </summary>
        public static string every_01_minute = " 0 * * ? * * ";
        /// <summary>
        /// every even minute
        /// </summary>
        public static string every_01_minutes_even = " 0 */2 * ? * * ";
        /// <summary>
        /// every uneven minute
        /// </summary>
        public static string every_minute_odd = " 0 1/2 * ? * * ";
        /// <summary>
        /// every 2 minutes
        /// </summary>
        public static string every_02_minutes = " 0 */2 * ? * * ";
        /// <summary>
        /// every 3 minutes
        /// </summary>
        public static string every_03_minutes = " 0 */3 * ? * * ";
        /// <summary>
        /// every 4 minutes
        /// </summary>
        public static string every_04_minutes = " 0 */4 * ? * * ";
        /// <summary>
        /// every 5 minutes
        /// </summary>
        public static string every_05_minutes = " 0 */5 * ? * * ";
        /// <summary>
        /// every 10 minutes
        /// </summary>
        public static string every_10_minutes = " 0 */10 * ? * * ";
        /// <summary>
        /// every 15 minutes
        /// </summary>
        public static string every_15_minutes = " 0 */15 * ? * * ";
        /// <summary>
        /// every 30 minutes
        /// </summary>
        public static string every_30_minutes = " 0 */30 * ? * * ";
        /// <summary>
        /// every hour at minutes 15 30 and 45
        /// </summary>
        public static string every_01_hour_at_minutes_15_30_and_45 = " 0 15,30,45 * ? * * ";
        /// <summary>
        /// every hour
        /// </summary>
        public static string every_01_hour = " 0 0 * ? * * ";
        //  public static string every_hour = " 0 0 */2 ? * * ";
        /// <summary>
        /// every even hour
        /// </summary>
        public static string every_01_hour_even = " 0 0 0/2 ? * * ";
        /// <summary>
        /// every uneven hour
        /// </summary>
        public static string every_01_hour_odd = " 0 0 1/2 ? * * ";
        /// <summary>
        /// every three hours
        /// </summary>
        public static string every_03_hours = " 0 0 */3 ? * * ";
        /// <summary>
        /// every four hours
        /// </summary>
        public static string every_04_hours = " 0 0 */4 ? * * ";
        /// <summary>
        /// every six hours
        /// </summary>
        public static string every_06_hours = " 0 0 */6 ? * * ";
        /// <summary>
        /// every eight hours
        /// </summary>
        public static string every_08_hours = " 0 0 */8 ? * * ";
        /// <summary>
        /// every twelve hours
        /// </summary>
        public static string every_12_hours = " 0 0 */12 ? * * ";
        /// <summary>
        /// every day at midnight 12am
        /// </summary>
        public static string every_day_at_12am = " 0 0 0 * * ? ";
        /// <summary>
        /// every day at 1am
        /// </summary>
        public static string every_day_at_01am = " 0 0 1 * * ? ";
        /// <summary>
        /// every day at 6am
        /// </summary>
        public static string every_day_at_06am = " 0 0 6 * * ? ";
       /// <summary>
        /// every day at noon 12PM
        /// </summary>
        public static string every_day_at_12pm = " 0 0 12 * * ? ";
        /// <summary>
        /// every sunday at noon
        /// </summary>
        public static string every_sunday_at_noon = " 0 0 12 * * SUN ";
        /// <summary>
        /// every monday at noon
        /// </summary>
        public static string every_monday_at_noon = " 0 0 12 * * MON ";
        /// <summary>
        /// every tuesday at noon
        /// </summary>
        public static string every_tuesday_at_noon = " 0 0 12 * * TUE ";
        /// <summary>
        /// every wednesday at noon
        /// </summary>
        public static string every_wednesday_at_noon = " 0 0 12 * * WED ";
        /// <summary>
        /// every thursday at noon
        /// </summary>
        public static string every_thursday_at_noon = " 0 0 12 * * THU ";
        /// <summary>
        /// every friday at noon
        /// </summary>
        public static string every_friday_at_noon = " 0 0 12 * * FRI ";
        /// <summary>
        /// every saturday at noon
        /// </summary>
        public static string every_saturday_at_noon = " 0 0 12 * * SAT ";
        /// <summary>
        /// every weekday at noon
        /// </summary>
        public static string every_weekday_at_noon = " 0 0 12 * * MON-FRI ";
        /// <summary>
        /// every saturday and sunday at noon
        /// </summary>
        public static string every_saturday_and_sunday_at_noon = " 0 0 12 * * SUN,SAT ";
        /// <summary>
        /// every 7 days at noon
        /// </summary>
        public static string every_7_days_at_noon = " 0 0 12 */7 * ? ";
        /// <summary>
        /// every month on 1ST at noon
        /// </summary>
        public static string every_month_on_the_1st_at_noon = " 0 0 12 1 * ? ";
        /// <summary>
        /// every month on 2ND at noon
        /// </summary>
        public static string every_month_on_the_2nd_at_noon = " 0 0 12 2 * ? ";
        /// <summary>
        /// every month on 15TH at noon
        /// </summary>
        public static string every_month_on_the_15th_at_noon = " 0 0 12 15 * ? ";
        /// <summary>
        /// every 2 months on 1ST at noon
        /// </summary>
        public static string every_2_months_on_the_1st_at_noon = " 0 0 12 1/2 * ? ";
        /// <summary>
        /// every 4 months on 1ST at noon
        /// </summary>
        public static string every_4_months_on_the_1st_at_noon = " 0 0 12 1/4 * ? ";
        /// <summary>
        /// every month on last day of month at noon
        /// </summary>
        public static string every_month_on_the_last_day_of_the_month_at_noon = " 0 0 12 L * ? ";
        /// <summary>
        /// every month on second to last day of month at noon
        /// </summary>
        public static string every_month_on_the_second_to_last_day_of_the_month_at_noon = " 0 0 12 L-2 * ? ";
        /// <summary>
        /// every month on last weekday at noon
        /// </summary>
        public static string every_month_on_the_last_weekday_at_noon = " 0 0 12 LW * ? ";
        /// <summary>
        /// every month on last sunday at noon
        /// </summary>
        public static string every_month_on_the_last_sunday_at_noon = " 0 0 12 1L * ? ";
        /// <summary>
        /// every month on last monday at noon
        /// </summary>
        public static string every_month_on_the_last_monday_at_noon = " 0 0 12 2L * ? ";
        /// <summary>
        /// every month on last friday at noon
        /// </summary>
        public static string every_month_on_the_last_friday_at_noon = " 0 0 12 6L * ? ";
        /// <summary>
        /// every month on nearest weekday to 1ST of month at noon
        /// </summary>
        public static string every_month_on_the_nearest_weekday_to_the_1st_of_the_month_at_noon = " 0 0 12 1W * ? ";
        /// <summary>
        /// every month on nearest weekday to 15TH of month at noon
        /// </summary>
        public static string every_month_on_the_nearest_weekday_to_the_15th_of_the_month_at_noon = " 0 0 12 15W * ? ";
        /// <summary>
        /// every month on first monday of month at noon
        /// </summary>
        public static string every_month_on_the_first_monday_of_the_month_at_noon = " 0 0 12 ? * 2#1 ";
        /// <summary>
        /// every month on first friday of month at noon
        /// </summary>
        public static string every_month_on_the_first_friday_of_the_month_at_noon = " 0 0 12 ? * 6#1 ";
        /// <summary>
        /// every month on second monday of month at noon
        /// </summary>
        public static string every_month_on_the_second_monday_of_the_month_at_noon = " 0 0 12 ? * 2#2 ";
        /// <summary>
        /// every month on third thursday of month at noon 12PM
        /// </summary>
        public static string every_month_on_the_third_thursday_of_the_month_at_noon_12pm = " 0 0 12 ? * 5#3 ";
        /// <summary>
        /// every day at noon in january only
        /// </summary>
        public static string every_day_at_noon_in_january_only = " 0 0 12 ? JAN * ";
        /// <summary>
        /// every day at noon in june only
        /// </summary>
        public static string every_day_at_noon_in_june_only = " 0 0 12 ? JUN * ";
        /// <summary>
        /// every day at noon in january and june
        /// </summary>
        public static string every_day_at_noon_in_january_and_june = " 0 0 12 ? JAN,JUN * ";
        /// <summary>
        /// every day at noon in december only
        /// </summary>
        public static string every_day_at_noon_in_december_only = " 0 0 12 ? DEC * ";
        /// <summary>
        /// every day at noon in january february march and april
        /// </summary>
        public static string every_day_at_noon_in_january_february_march_and_april = " 0 0 12 ? JAN,FEB,MAR,APR * ";
        /// <summary>
        /// every day at noon between september and december
        /// </summary>
        public static string every_day_at_noon_between_september_and_december = " 0 0 12 ? 9-12 * ";


    }
}
