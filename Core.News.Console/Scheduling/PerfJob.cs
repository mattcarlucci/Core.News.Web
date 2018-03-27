// ***********************************************************************
// Assembly         : Core.News.Console
// Author           : mcarlucci
// Created          : 03-26-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-26-2018
// ***********************************************************************
// <copyright file="PerfJob.cs" company="Core.News.Console">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.News.Console.Scheduling
{
    /// <summary>
    /// Class PerfJob.
    /// </summary>
    /// <seealso cref="Quartz.IJob" />
    public class PerfJob: IJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerfJob"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PerfJob(ILogger<PerfJob> logger)
        {         
            _logger = logger;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public IJobExecutionContext context { get; private set; }
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        static ILogger<PerfJob> _logger { get; set; }

        /// <summary>
        /// Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.ITrigger" />
        /// fires that is associated with the <see cref="T:Quartz.IJob" />.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <returns>Task.</returns>
        /// <remarks>The implementation may wish to set a  result object on the
        /// JobExecutionContext before this method exits.  The result itself
        /// is meaningless to Quartz, but may be informative to
        /// <see cref="T:Quartz.IJobListener" />s or
        /// <see cref="T:Quartz.ITriggerListener" />s that are watching the job's
        /// execution.</remarks>
        public Task Execute(IJobExecutionContext context)
        {
            this.context = context;
            return Task.Run(Execute);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task Execute()
        {
            var process = Process.GetCurrentProcess();
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("\r\nProcess Id: {0}\r\nWorking set {1:#,###} KB\r\n", process.Id, process.WorkingSet64 / 1024);
            sb.AppendFormat("Private Memory {0:#,###} KB\r\n", process.PrivateMemorySize64 / 1024);
            sb.AppendFormat("Peak Working set {0:#,###} KB\r\n", process.PeakWorkingSet64 / 1024 );
            sb.AppendFormat("Total CPU time {0:#,###} sec\r\n", process.TotalProcessorTime.TotalSeconds);
            sb.AppendFormat("Total number of Thread {0}\r\n", process.Threads.Count);
            sb.AppendFormat("GC Total Memory {0:#,###} KB\r\n", GC.GetTotalMemory(false) / 1024);
            sb.AppendFormat("GC Count Gen 0 {0}\r\n", GC.CollectionCount(0));
            sb.AppendFormat("GC Count Gen 1 {0}\r\n", GC.CollectionCount(1)); 

            _logger.LogInformation(sb.ToString());
            await Task.FromResult(0);
        }

        /// <summary>
        /// Gets the process information.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetProcessInfo() 
        {
            var stat = JsonConvert.SerializeObject(Process.GetCurrentProcess(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    Error = (s, e) =>
                    {
                        var error = e.ErrorContext.Error.Message;
                        Debug.Print(error + "\r\n");
                        e.ErrorContext.Handled = true;
                    }
                });
            return stat;
        }

        /// <summary>
        /// Profiles the specified code block.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codeBlock">The code block.</param>
        /// <param name="description">The description.</param>
        /// <returns>T.</returns>
        public static T Profile<T>(Func<T> codeBlock, string description = "")
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            T res = codeBlock();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            const double thresholdSec = 2;
            double elapsed = ts.TotalSeconds;
            if (elapsed > thresholdSec)
                _logger.LogDebug(description + " code was too slow! It took " + elapsed + " second(s).");

            return res;
        }
    }
}
