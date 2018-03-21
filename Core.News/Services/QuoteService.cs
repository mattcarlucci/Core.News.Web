
using Core.News.Services;
using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.News.Services
{
    public class QuoteService : HostedService
    {
        private readonly QuoteProvider provider;
        private readonly ILogger<QuoteService> logger;

        public QuoteService(QuoteProvider quoteProvider, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<QuoteService>();
            logger.LogInformation("Quote Service Started.");
            provider = quoteProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("Quote Service is Executing...");
                await provider.ExecuteAsync(cancellationToken);
                logger.LogInformation(provider.ToString());
                await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);
            }
        }
    }
}
