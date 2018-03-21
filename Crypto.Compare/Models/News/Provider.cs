using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Compare.Models
{
    public class Provider
    {
        public string Key { get; set; }
        public string Name { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("img")]
        public string ImgUrl { get; set; }

    }
}
