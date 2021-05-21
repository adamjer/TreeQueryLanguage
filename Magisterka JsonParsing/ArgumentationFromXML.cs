using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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

        public abstract class Node
        {
            [JsonProperty("@id")]
            public String ID { get; set; }
            [JsonProperty("name")]
            public String Name { get; set; }
            [JsonProperty("label")]
            public String Label { get; set; }
            [JsonProperty("description")]
            public String Description { get; set; }
            [JsonProperty("nodes")]
            public Nodes Children { get; set; }
            [JsonProperty("weight")]
            public String Weight { get; set; }
            [JsonProperty("assessment")]
            public Assessment Assessment { get; set; }
            [JsonProperty("assessmentHistory")]
            public AssessmentHistory? AssessmentHistory { get; set; }
            //Strategy
            [JsonProperty("counterArgumentation")]
            public String CounterArgumentation { get; set; }
            //Rationale
            [JsonProperty("aggregationRule")]
            public String AggregationRule { get; set; }

            public Node Parent { get; set; }

            public Node()
            {
                this.Parent = null;
                this.ID = null;
                this.Label = null;
                this.Name = null;
                this.Description = null;
                this.Children = new Nodes();
            }

            public bool isRoot()
            {
                return this.Parent == null;
            }

            public bool isLeaf()
            {
                return this.Children.Count == 0;
            }

            public IEnumerable<Node> Descendants()
            {
                for (int i = 0; i < this.Children.Count; i++)
                {
                    yield return this.Children.At(i);

                    var descendants = this.Children.At(i).Descendants();
                    foreach (var grandChild in descendants)
                    {
                        yield return grandChild;
                    }
                }
            }

            public IEnumerable<Node> Ascendants()
            {
                var parent = this.Parent;
                while (parent != null)
                {
                    yield return parent;
                    parent = parent.Parent;
                }
            }

            public IEnumerable<Node> Elements()
            {
                for (int i = 0; i < this.Children.Count; i++)
                {
                    yield return this.Children.At(i);
                }
            }
        }

        public class Nodes
        {
            [JsonProperty("node")]
            private List<Node> _nodes { get; set; }
            public int Count { get { return _nodes.Count; } }

            public Nodes()
            {
                this._nodes = new List<Node>();
            }

            public Nodes(Node node)
            {
                this._nodes = new List<Node>();
                this._nodes.Add(node);
            }

            public Nodes(List<Node> nodes)
            {
                this._nodes = new List<Node>(nodes.Count);
                for(int i = 0; i < nodes.Count; i++)
                {
                    this._nodes[i] = nodes[i];
                }
            }

            public Node At(int index)
            {
                try
                {
                    return this._nodes.ElementAt(index);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.Out.WriteLine(e.Message);
                }

                throw new Exception("Error: Exception in function At() in class Nodes");
            }

            public void Add(Node node)
            {
                try
                {
                    this._nodes.Add(node);
                    return;
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                }

                throw new Exception("Error: Exception in function Add() in class Nodes");
            }
        }

        public class Root : Node
        {
            public Root()
            {
                this.Parent = null;
                this.ID = "root";
                this.Label = "root";
                this.Name = "root";
                this.Description = "root";
                this.Children = new Nodes();
            }
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
            private List<Assessment> _assessments { get; set; }

            public Assessment At(int index)
            {
                try
                {
                    return this._assessments.ElementAt(index);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.Out.WriteLine(e.Message);
                }

                throw new Exception("Error: Exception in function At() in class AssessmentHistory");
            }
        }

        public class Claim : Node
        {
            
        }

        public class Strategy : Node
        {

        }

        public class Rationale : Node
        {

        }

        public class Fact : Node
        {

        }

        public class Reference : Node
        {
            public String RepositoryName { get; set; }
            public String Repository { get; set; }
            public String Address { get; set; }
            public String AutoUpdate { get; set; }
            public DateTime? ValidityDate { get; set; }
            public DateTime? MaxvalidityPeriod { get; set; }
        }

        public class Assumption : Node
        {

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

            public void Init()
            {
                this.InitParents(this.Root);
            }

            private void InitParents(Node Parent)
            {
                for (int i = 0; i < Parent.Children.Count; i++)
                {
                    Parent.Children.At(i).Parent = Parent;
                    InitParents(Parent.Children.At(i));
                }
            }
        }

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

        public class Sections
        {
            [JsonProperty("section")]
            private List<Section> Section { get; set; }

            public Section At(int index)
            {
                try
                {
                    return this.Section.ElementAt(index);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.Out.WriteLine(e.Message);
                }

                throw new Exception("Error: Exception in function At() in class Sections");
            }
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
