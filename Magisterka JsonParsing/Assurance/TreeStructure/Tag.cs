using Newtonsoft.Json;
using System;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
    public class Tag
    {
        [JsonProperty("@name")]
        public String Name { get; set; }
        [JsonProperty("#text")]
        public String Text { get; set; }
    }
}
