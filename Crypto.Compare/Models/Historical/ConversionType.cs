using Newtonsoft.Json;

namespace Crypto.Compare.Models.Historical
{
    public class ConversionType
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("conversionSymbol")]
        public string ConversionSymbol { get; set; }
    }
   
}