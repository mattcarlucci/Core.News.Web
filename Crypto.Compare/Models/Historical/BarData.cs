using Core.News.Converters;
using Newtonsoft.Json;
using System;

namespace Crypto.Compare.Models.Historical
{
    public class BarData
    {

        [JsonProperty("time")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime TimeId { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("volumefrom")]
        public double Volumefrom { get; set; }

        [JsonProperty("volumeto")]
        public double Volumeto { get; set; }
    }
   
}