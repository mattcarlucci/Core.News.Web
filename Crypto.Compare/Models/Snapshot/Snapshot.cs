using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Compare.Models.Snapshot
{
    public class ShapShot
    {

        [JsonProperty("Algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("ProofType")]
        public string ProofType { get; set; }

        [JsonProperty("BlockNumber")]
        public int BlockNumber { get; set; }

        [JsonProperty("NetHashesPerSecond")]
        public double NetHashesPerSecond { get; set; }

        [JsonProperty("TotalCoinsMined")]
        public double TotalCoinsMined { get; set; }

        [JsonProperty("BlockReward")]
        public double BlockReward { get; set; }

        [JsonProperty("AggregatedData")]
        public AggregatedData AggregatedData { get; set; }

        [JsonProperty("Exchanges")]
        public List<Exchange> Exchanges { get; set; }
    }
}
