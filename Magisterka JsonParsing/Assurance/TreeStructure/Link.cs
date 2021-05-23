using Newtonsoft.Json;
using System;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
    public class Target
    {
        [JsonProperty("@id")]
        public String ID { get; set; }
        [JsonProperty("@type")]
        public String Type { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("label")]
        public String Label { get; set; }
    }

    public class Link : Node
    {
        public Link()
        {
            this.Target = new Target();
        }
    }
}
