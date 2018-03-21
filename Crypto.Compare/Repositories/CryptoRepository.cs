using Crypto.Compare.Models;
using Crypto.Compare.Models.CoinInfo;
using Crypto.Compare.Models.Historical;
using Crypto.Compare.Models.SocialStats;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Compare.Repositories
{
    /// <summary>
    /// Class CryptoRepository.
    /// </summary>
    public class CryptoRepository
    {
        //TODO: Add to DI and wire up necessary classes
        public CryptoRepository()
        {

        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>JToken.</returns>
        private JToken GetDataToken(string url)
        {
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(url);
                JObject obj = JObject.Parse(json);
                return obj["Data"];
            }
        }
        /// <summary>
        /// Gets the crypto coins.
        /// </summary>
        /// <returns>List&lt;CryptoCoin&gt;.</returns>
        public List<CryptoCoin> GetCryptoCoins()
        {
            ConcurrentBag<CryptoCoin> coins = new ConcurrentBag<CryptoCoin>();

            string url = "https://www.cryptocompare.com/api/data/coinlist/";
            var data = GetDataToken(url);
            Parallel.ForEach(data, item =>
            {
                coins.Add(JsonConvert.DeserializeObject<CryptoCoin>(item.First.ToString()));
            });         
            return coins.ToList();
        }

        /// <summary>
        /// Transforms the specified URL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="web">The web.</param>
        /// <returns>T.</returns>
        private T Transform<T>(string url) where T: class
        {          
            T stat = JsonConvert.DeserializeObject<T>(GetDataToken(url).ToString(),
                new JsonSerializerSettings
            {
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
        /// Gets the social stats.
        /// </summary>
        /// <param name="coins">The coins.</param>
        /// <returns>List&lt;SocialStats&gt;.</returns>
        public List<SocialStats> GetSocialStats(List<CryptoCoin> coins)
        {
            ConcurrentBag<SocialStats> bag = new ConcurrentBag<SocialStats>();
            Parallel.ForEach(coins, coin =>         
            {
                var url = "https://www.cryptocompare.com/api/data/socialstats/?id=" + coin.Id;
                var stat = Transform<SocialStats>(url);
                bag.Add(stat);                
            });
            return bag.ToList();
        }

        /// <summary>
        /// Gets the coin snapshot.
        /// </summary>
        /// <returns>CoinSnapshot.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<CoinSnapshot> GetCoinSnapshot(List<CryptoCoin> coins)
        {
            ConcurrentBag<string> bad = new ConcurrentBag<string>();
            ConcurrentBag<CoinSnapshot> bag = new ConcurrentBag<CoinSnapshot>();
            Parallel.ForEach(coins, coin =>
            {
                string url = string.Format("https://min-api.cryptocompare.com/data/top/exchanges/full?fsym={0}&tsym=USD", coin.Symbol);                
                var stat = Transform<CoinSnapshot>(url);
                if (stat != null) bag.Add(stat); else bad.Add(coin.Symbol);
                
            });
            return bag.ToList();
        }
        /// <summary>
        /// Gets the historical.
        /// </summary>
        /// <param name="coins">The coins.</param>
        /// <returns>List&lt;HistoricalData&gt;.</returns>
        public (List<T>, List<string>) GetHistorical<T>(List<CryptoCoin> coins, string type) where T: HistoricalBars
        {
            ConcurrentBag<string> bad = new ConcurrentBag<string>();
            ConcurrentBag<T> bag = new ConcurrentBag<T>();
            Parallel.ForEach(coins, coin =>
            {
                string url = string.Format("https://min-api.cryptocompare.com/data/{0}?fsym={1}&tsym=USD&limit=2000", type, coin.Symbol);
                var stat = Transform<T>(url);
                if (stat != null && stat.Bars.Sum(s => s.High) > 0)
                {
                    stat.Symbol = coin;
                    stat.Bars.Where(w => w.Open + w.High + w.Low == 0).
                        ToList().ForEach(item => stat.Bars.Remove(item));
                    if (stat.Bars.Count() > 0) bag.Add(stat);
                }
                else bad.Add(coin.Symbol);                
            });
            return (bag.ToList(), bad.ToList());
        }
    }
}
