using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing.Assurance
{
    public class Header
    {
        [JsonProperty("userName")]
        public String UserName { get; set; }
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
        [JsonProperty("generator")]
        public String Generator { get; set; }
        [JsonProperty("locale")]
        public String Locale { get; set; }
    }
}
