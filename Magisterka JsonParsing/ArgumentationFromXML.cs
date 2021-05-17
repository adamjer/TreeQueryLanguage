using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing
{
    namespace ArgumentationFromXML
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

        public class Nodes
        {
            [JsonProperty("node")]
            public List<Node> Node { get; set; }
        }

        public abstract class Node
        {
            [JsonProperty("@id")]
            public String ID { get; set; }
            [JsonProperty("@type")]
            //public String Type { get; set; }
            //[JsonProperty("name")]
            public String Name { get; set; }
            [JsonProperty("label")]
            public String Label { get; set; }
            [JsonProperty("description")]
            public String Description { get; set; }
            [JsonProperty("nodes")]
            public Nodes Nodes { get; set; }

        }

        public class Information : Node
        {
            
        }

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
        }

        public class AssessmentHistory
        {
            [JsonProperty("assessment")]
            public List<Assessment> Assessments { get; set; }
        }

        public class Claim : Node
        {
            public String Weight { get; set; }
            public Assessment Assessment { get; set; }
            public AssessmentHistory AssessmentHistory { get; set; }
        }

        public class Strategy : Node
        {
            public String CounterArgumentation { get; set; }
            public Assessment Assessment { get; set; }
            public AssessmentHistory AssessmentHistory { get; set; }
        }

        public class Rationale : Node
        {
            public String AggregationRule { get; set; }
            public Assessment Assessment { get; set; }
            public AssessmentHistory AssessmentHistory { get; set; }
        }

        public class Fact : Node
        {
            public String Weight { get; set; }
            public Assessment Assessment { get; set; }
            public AssessmentHistory AssessmentHistory { get; set; }
        }

        public class Reference : Node
        {
            public String RepositoryName { get; set; }
            public String Repository { get; set; }
            public String Address { get; set; }
            public String AutoUpdate { get; set; }
            public String ValidityDate { get; set; } //originally DateTime
            public String MaxvalidityPeriod { get; set; } //originally DateTime
        }

        public class Assumption : Node
        {
            public String Weight { get; set; }
            public Assessment Assessment { get; set; }
            public AssessmentHistory AssessmentHistory { get; set; }
        }

        public class AssessmentMethod
        {
            [JsonProperty("@type")]
            public String Type { get; set; }
            [JsonProperty("#text")]
            public String Text { get; set; }
        }

        public class Project
        {
            [JsonProperty("@id")]
            public String ID { get; set; }
            [JsonProperty("name")]
            public String Name { get; set; }
            [JsonProperty("symbol")]
            public String Symbol { get; set; }
            [JsonProperty("folder")]
            public String Folder { get; set; }
            [JsonProperty("path")]
            public String Path { get; set; }
            [JsonProperty("assessmentMethod")]
            public AssessmentMethod AssessmentMethod { get; set; }
            [JsonProperty("description")]
            public String Description { get; set; }
            [JsonProperty("node")]
            public Node Root { get; set; }


        }

        public class SectionElement
        {
            [JsonProperty("@id")]
            String ID { get; set; }
        }

        public class Section
        {
            [JsonProperty("@id")]
            public String ID { get; set; }
            [JsonProperty("parentSectionId")]
            public String ParentSectionID { get; set; }
            [JsonProperty("mainElementId")]
            public String MainElementID { get; set; }
            [JsonProperty("sectionElement")]
            public List<SectionElement> SectionElements { get; set; }


        }

        public class Sections
        {
            [JsonProperty("section")]
            List<Section> Section { get; set; }
        }

        public class Report
        {
            [JsonProperty("header")]
            public Header Header { get; set; }
            [JsonProperty("project")]
            public Project Project { get; set; }
            [JsonProperty("sections")]
            public Sections Sections { get; set; }
        }

        public class AssuranceCase
        {
            [JsonProperty("report")]
            public Report Report { get; set; }
        }

        public class ArgumentsConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return typeof(Node).IsAssignableFrom(objectType);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject jo = JObject.Load(reader);

                string type = (string)jo["@type"];

                Node item;
                type = type.ToLower().Trim();
                if (type == "claim")
                    item = new Claim();
                else if (type == "strategy")
                    item = new Strategy();
                else if (type == "information")
                    item = new Information();
                else if (type == "rationale")
                    item = new Rationale();
                else if (type == "fact")
                    item = new Fact();
                else if (type == "reference")
                    item = new Reference();
                else if (type == "assumption")
                    item = new Assumption();
                else
                    throw new Exception($"Type {type} not implemented!");

                serializer.Populate(jo.CreateReader(), item);

                return item;
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
