using Newtonsoft.Json;
using System.Collections.Generic;

namespace Crypto.Compare.Models.CoinInfo
{
    public class CoinSnapshot
    {

        [JsonProperty("CoinInfo")]
        public CoinInfo CoinInfo { get; set; }

        [JsonProperty("AggregatedData")]
        public AggregatedData AggregatedData { get; set; }

        [JsonProperty("Exchanges")]
        public IList<Exchange> Exchanges { get; set; }
    }


}
