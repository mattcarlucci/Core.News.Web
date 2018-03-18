using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Crypto.Compare
{
    public interface ILogger
    {
        void Show(Action action);
    }

   
    /// <summary>
    /// Class StopWatchEventArgs.
    /// </summary>
    public class StopWatchEventArgs : EventArgs, ILogger
    {
        /// <summary>
        /// Gets or sets the watch.
        /// </summary>
        /// <value>The watch.</value>
        public Stopwatch Watch { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopWatchEventArgs"/> class.
        /// </summary>
        /// <param name="watch">The watch.</param>
        public StopWatchEventArgs(Stopwatch watch)
        {
            this.Watch = watch;
        }

        /// <summary>
        /// Creates the specified watch.
        /// </summary>
        /// <param name="watch">The watch.</param>
        /// <returns>StopWatchEventArgs.</returns>
        internal static StopWatchEventArgs Create(Stopwatch watch)
        {
            return new StopWatchEventArgs(watch);
        }

        /// <summary>
        /// Shows the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Show(Action action)
        {
            action.Invoke();
        }
    }
}
