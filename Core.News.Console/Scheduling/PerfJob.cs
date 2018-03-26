using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Core.News.Console.Scheduling
{
    public class PerfJob: IJob
    {
        public PerfJob(ILogger<PerfJob> logger)
        {         
            this.logger = logger;
        }

        public IJobExecutionContext context { get; private set; }
        public ILogger<PerfJob> logger { get; }

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

        private async Task Execute()
        {
            var me = Process.GetCurrentProcess();
            logger.LogInformation("Working set {0:#,###} MB", me.WorkingSet64 / 1024 / 1024);
            logger.LogInformation("Total CPU time {0:#,###} sec", me.TotalProcessorTime.TotalSeconds);
            await Task.FromResult(0);
        }
    }
}
