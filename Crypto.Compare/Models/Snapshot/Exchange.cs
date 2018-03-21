using Core.News.Converters;
using Newtonsoft.Json;
using System;

namespace Crypto.Compare.Models.Snapshot
{
    public class Exchange
    {

        [JsonProperty("TYPE")]
        public int Type { get; set; }

        [JsonProperty("MARKET")]
        public string Market { get; set; }

        [JsonProperty("FROMSYMBOL")]
        public string FromSymbol { get; set; }

        [JsonProperty("TOSYMBOL")]
        public string ToSymbol { get; set; }

        [JsonProperty("FLAGS")]
        public int Flags { get; set; }

        [JsonProperty("PRICE")]
        public float Price { get; set; }

        [JsonProperty("LASTUPDATE")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("LASTVOLUME")]
        public double LastVolume { get; set; }

        [JsonProperty("LASTVOLUMETO")]
        public double LastVolumeTo { get; set; }

        [JsonProperty("LASTTRADEID")]
        public double LastTradeId { get; set; }

        [JsonProperty("VOLUME24HOUR")]
        public double Volume24Hour { get; set; }

        [JsonProperty("VOLUME24HOURTO")]
        public double Volume24HourTo { get; set; }

        [JsonProperty("OPEN24HOUR")]
        public double Open24Hour { get; set; }

        [JsonProperty("HIGH24HOUR")]
        public double High24Hour { get; set; }

        [JsonProperty("LOW24HOUR")]
        public double Low24Hour { get; set; }
    }
}
