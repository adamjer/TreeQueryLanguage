using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing.Assurance
{
    public class Report
    {
        [JsonProperty("header")]
        public Header Header { get; set; }
        [JsonProperty("project")]
        public Project Project { get; set; }
        [JsonProperty("sections")]
        public Sections Sections { get; set; }
    }
}
