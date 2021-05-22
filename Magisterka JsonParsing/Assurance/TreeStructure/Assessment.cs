using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
        public class Confidence
        {
            [JsonProperty("@degree")]
            public String Degree { get; set; }
            [JsonProperty("@maxDegree")]
            public String MaxDegree { get; set; }
            [JsonProperty("@value")]
            public String Value { get; set; }
            [JsonProperty("#text")]
            public String Text { get; set; }
        }

        public class Decision
        {
            [JsonProperty("@degree")]
            public String Degree { get; set; }
            [JsonProperty("@maxDegree")]
            public String MaxDegree { get; set; }
            [JsonProperty("@value")]
            public String Value { get; set; }
            [JsonProperty("#text")]
            public String Text { get; set; }
        }

        public class Assessment
        {
            [JsonProperty("@isChange")]
            public String IsChange { get; set; }
            [JsonProperty("confidence")]
            public Confidence Confidence { get; set; }
            [JsonProperty("decision")]
            public Decision Decision { get; set; }
            [JsonProperty("comment")]
            public String Comment { get; set; }
            [JsonProperty("excluded")]
            public String Excluded { get; set; }
            [JsonProperty("username")]
            public String UserName { get; set; }
            [JsonProperty("timestamp")]
            public DateTime TimeStamp { get; set; }

            public Assessment()
            {
                this.Confidence = new Confidence();
                this.Decision = new Decision();
            }
        }
    }

