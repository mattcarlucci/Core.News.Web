using Core.News.Converters;
using Newtonsoft.Json;
using System;

namespace Crypto.Compare.Models.CoinInfo
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

        [JsonProperty("CHANGE24HOUR")]
        public double Change24Hour { get; set; }

        [JsonProperty("CHANGEPCT24HOUR")]
        public double ChangePct24Hour { get; set; }

        [JsonProperty("CHANGEDAY")]
        public int ChangeDay { get; set; }

        [JsonProperty("CHANGEPCTDAY")]
        public int ChangePctDay { get; set; }

        [JsonProperty("SUPPLY")]
        public int Supply { get; set; }

        [JsonProperty("MKTCAP")]
        public double MarketCap { get; set; }

        [JsonProperty("TOTALVOLUME24H")]
        public double TotalVolume24Hour { get; set; }

        [JsonProperty("TOTALVOLUME24HTO")]
        public double TotalVolume24HourTo { get; set; }
    }


}
