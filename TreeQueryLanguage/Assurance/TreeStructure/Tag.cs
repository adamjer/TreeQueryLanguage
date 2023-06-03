using Newtonsoft.Json;
using System;

namespace TreeQueryLanguage.Assurance.TreeStructure
{
    public class Tag
    {
        [JsonProperty("@name")]
        public String Name { get; set; }
        [JsonProperty("#text")]
        public String Text { get; set; }
    }
}
