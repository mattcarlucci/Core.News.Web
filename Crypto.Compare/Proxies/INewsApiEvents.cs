using System;

namespace Crypto.Compare.Proxies
{
    public interface INewsApiEvents
    {
        EventHandler<NewsCompleteEventArgs> NewsComplete { get; set; }
        EventHandler<NewsDetailEventArgs> NewsDetail { get; set; }
        EventHandler<NewsCompleteEventArgs> NewsDetailComplete { get; set; }
        EventHandler<StopWatchEventArgs> NewsStart { get; set; }
        EventHandler<NewsSummaryEventArgs> NewsSummary { get; set; }
        EventHandler<NewsCompleteEventArgs> NewsSummaryComplete { get; set; }
        UnhandledExceptionEventHandler Exception { get; set; }
    }
}