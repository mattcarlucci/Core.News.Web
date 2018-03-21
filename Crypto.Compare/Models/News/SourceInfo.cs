using Newtonsoft.Json;

namespace Crypto.Compare.Models
{
    public partial class Publication
    {
        public class SourceInfo
        {
            public string Name { get; set; }
            [JsonProperty("lang")]
            public string Language { get; set; }
            [JsonProperty("img")]
            public string ImageUrl { get; set; }
        }
    }
}
