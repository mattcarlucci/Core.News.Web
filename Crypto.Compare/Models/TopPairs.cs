using Newtonsoft.Json;

namespace Crypto.Compare.Models
{
    public class TopPairs
    {
        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("fromSymbol")]
        public string FromSymbol { get; set; }

        [JsonProperty("toSymbol")]
        public string ToSymbol { get; set; }

        [JsonProperty("volume24h")]
        public double Volume24h { get; set; }

        [JsonProperty("volume24hTo")]
        public double Volume24hTo { get; set; }
    }
}
