using Newtonsoft.Json;

namespace Crypto.Compare.Models.CoinInfo
{
    public class Header
    {
        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public CoinSnapshot Data { get; set; }
    }


}
