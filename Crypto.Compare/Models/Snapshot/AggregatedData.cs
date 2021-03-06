﻿using Core.News.Converters;
using Newtonsoft.Json;
using System;

namespace Crypto.Compare.Models.Snapshot
{
    public class AggregatedData
    {

        [JsonProperty("TYPE")]
        public string Type { get; set; }

        [JsonProperty("MARKET")]
        public string Market { get; set; }

        [JsonProperty("FROMSYMBOL")]
        public string FromSymbol { get; set; }

        [JsonProperty("TOSYMBOL")]
        public string ToSymbol { get; set; }

        [JsonProperty("FLAGS")]
        public int Flags { get; set; }

        [JsonProperty("PRICE")]
        public double Price { get; set; }

        [JsonProperty("LASTUPDATE")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("LASTVOLUME")]
        public double LastVolume { get; set; }

        [JsonProperty("LASTVOLUMETO")]
        public double LastVolumeTo { get; set; }

        [JsonProperty("LASTTRADEID")]
        public int LastTradeId { get; set; }

        [JsonProperty("VOLUMEDAY")]
        public double VolumeDay { get; set; }

        [JsonProperty("VOLUMEDAYTO")]
        public double VolumeDayTo { get; set; }

        [JsonProperty("VOLUME24HOUR")]
        public double Volume24Hour { get; set; }

        [JsonProperty("VOLUME24HOURTO")]
        public double Volume24HourTo { get; set; }

        [JsonProperty("OPENDAY")]
        public double OpenDay { get; set; }

        [JsonProperty("HIGHDAY")]
        public double HighDay { get; set; }

        [JsonProperty("LOWDAY")]
        public double LowDay { get; set; }

        [JsonProperty("OPEN24HOUR")]
        public double Open24Hour { get; set; }

        [JsonProperty("HIGH24HOUR")]
        public double High24Hour { get; set; }

        [JsonProperty("LOW24HOUR")]
        public double Low24Hour { get; set; }

        [JsonProperty("LASTMARKET")]
        public double LastMarket { get; set; }
    }
}
