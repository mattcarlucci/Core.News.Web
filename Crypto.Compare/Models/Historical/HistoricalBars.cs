using Core.News.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Crypto.Compare.Models.Historical
{
    public class HistoricalBars
    {
        public virtual CryptoCoin Symbol { get; set; }

        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("Type")]
        public int Type { get; set; }

        [JsonProperty("Aggregated")]
        public bool Aggregated { get; set; }
        
        [JsonProperty("Data")]
        public IList<BarData> Bars { get; set; }

        [JsonProperty("TimeTo")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime TimeTo { get; set; }

        [JsonProperty("TimeFrom")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime TimeFrom { get; set; }

        [JsonProperty("FirstValueInArray")]
        public bool FirstValueInArray { get; set; }

        [JsonProperty("ConversionType")]
        public ConversionType ConversionType { get; set; }
    }
   
}