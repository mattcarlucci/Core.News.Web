using Newtonsoft.Json;
using System.Text;

namespace Crypto.Compare.Models.CoinInfo
{
    public class CoinInfo
    {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Internal")]
        public string Internal { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("Algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("ProofType")]
        public string ProofType { get; set; }

        [JsonProperty("TotalCoinsMined")]
        public int TotalCoinsMined { get; set; }

        [JsonProperty("BlockNumber")]
        public string BlockNumber { get; set; }

        [JsonProperty("NetHashesPerSecond")]
        public double NetHashesPerSecond { get; set; }

        [JsonProperty("BlockReward")]
        public double BlockReward { get; set; }

        [JsonProperty("TotalVolume24H")]
        public double TotalVolume24H { get; set; }
    }
}
