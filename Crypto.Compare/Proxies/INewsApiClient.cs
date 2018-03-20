using System;

namespace Crypto.Compare.Proxies
{
    public interface INewsApiClient
    {
        int StoryCount { get; set; }

        void RequestLatestNews();
    }
}