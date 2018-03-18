using System;

namespace Crypto.Compare.Proxies
{
    public interface IWebApiClient
    {
        int StoryCount { get; set; }

        void RequestLatestNews();
    }
}