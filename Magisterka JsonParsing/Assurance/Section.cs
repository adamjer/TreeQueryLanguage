using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing.Assurance
{
    public class SectionElement
    {
        [JsonProperty("@id")]
        public String ID { get; set; }
    }

    public class Section
    {
        [JsonProperty("@id")]
        public String ID { get; set; }
        [JsonProperty("@parentSectionId")]
        public String ParentSectionID { get; set; }
        [JsonProperty("@mainElementId")]
        public String MainElementID { get; set; }
        [JsonProperty("sectionElement")]
        public List<SectionElement> SectionElements { get; set; }

    }
}
