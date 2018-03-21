using Core.News.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.News.Services
{
    public class NewsEmailProvider : IScheduledTask
    {
        public string Schedule => "* */6 * * *";

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //TODO: This is where we will send emails
            //need a db context, logger context and posible logger context
            throw new NotImplementedException();
        }
    }
    public class QuoteProviderMock : IScheduledTask
    {
        public string Schedule => CronExprs.every_10_seconds;// "* */6 * * *";
        

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Running Quote Serivce  {0}", 
                CronExpressionDescriptor.ExpressionDescriptor.GetDescription(Schedule));

            Task t = Task.Run(() =>
            {
                QuoteOfTheDay.Current = new QuoteOfTheDay() { Quote = "This is a mock", Author = "Matt" };
            });
            t.Wait();
            await t;

        }
        public override string ToString()
        {
            return QuoteOfTheDay.Current.Quote;
        }
    }

    public class QuoteProvider : IScheduledTask
    {
        public string Schedule => "* */6 * * *";
       
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {           
            using (HttpClient httpClient = new HttpClient())
            {
                var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));

                QuoteOfTheDay.Current = JsonConvert.DeserializeObject<QuoteOfTheDay>
                    (quoteJson["contents"]["quotes"][0].ToString());
            }

        }
        public override string ToString()
        {
            return QuoteOfTheDay.Current.Quote;
        }
    }
}
