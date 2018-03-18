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
    public class QuoteProviderMock : IScheduledTask
    {
        public string Schedule => "* */6 * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
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
