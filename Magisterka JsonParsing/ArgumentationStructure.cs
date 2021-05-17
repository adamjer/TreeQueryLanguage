using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magisterka_JsonParsing
{
    namespace ArgumentationV1
    {
        public class Claim : Node
        {

        }

        public class Strategy : Node
        {

        }

        public class Justification : Node
        {

        }

        public class Evidence : Node
        {
            [JsonProperty("filename")]
            public string Filename { get; set; }
        }

        public abstract class Node
        {
            public Node()
            {
                ParentId = -1;
                Id = -2;
                //Type = "NotInitialized";
                Label = "NotInitalized";
                Name = "NotInitilized";
            }

            public bool IsRoot()
            {
                if (this.ParentId == -1)
                    return true;
                return false;
            }

            //[JsonProperty("type")]
            //public string Type { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("parent")]
            public int ParentId { get; set; }
        }

        public class Argument
        {

            [JsonProperty("nodes")]
            public IList<Node> Nodes { get; set; }
        }

        public class AssuranceCase
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("arguments")]
            public IList<Argument> Arguments { get; set; }
        }

        public class ArgumentationStructure
        {
            [JsonProperty("assurance_case")]
            public AssuranceCase AssuranceCase { get; set; }
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

                string type = (string)jo["type"];

                Node item;
                type = type.ToLower().Trim();
                if (type == "claim")
                    item = new Claim();
                else if (type == "strategy")
                    item = new Strategy();
                else if (type == "evidence")
                    item = new Evidence();
                else if (type == "justification")
                    item = new Justification();
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

    namespace ArgumentationV2
    {
        public class Claim : Node
        {

        }

        public class Strategy : Node
        {

        }

        public class Justification : Node
        {

        }

        public class Evidence : Node
        {
            [JsonProperty("filename")]
            public string Filename { get; set; }
        }

        public abstract class Node
        {
            public Node()
            {
                ParentId = -1;
                Id = -2;
                Type = "NotInitialized";
                Label = "NotInitalized";
                Name = "NotInitilized";
            }

            public bool IsRoot()
            {
                if (this.ParentId == -1)
                    return true;
                return false;
            }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("parent")]
            public int ParentId { get; set; }
        }

        public class AssuranceCase
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("arguments")]
            public IList<Node> Nodes { get; set; }
        }

        public class ArgumentationStructure
        {
            [JsonProperty("assurance_case")]
            public AssuranceCase AssuranceCase { get; set; }

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

                string type = (string)jo["type"];

                Node item;
                type = type.ToLower().Trim();
                if (type == "claim")
                    item = new Claim();
                else if (type == "strategy")
                    item = new Strategy();
                else if (type == "evidence")
                    item = new Evidence();
                else if (type == "justification")
                    item = new Justification();
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

    namespace ArgumentationV3
    {
        public class Claim : Node
        {

        }

        public class Strategy : Node
        {

        }

        public class Justification : Node
        {

        }

        public class Evidence : Node
        {
            [JsonProperty("filename")]
            public string Filename { get; set; }
        }

        public abstract class Node
        {
            public Node()
            {
                ParentId = -1;
                Id = -2;
                Type = "NotInitialized";
                Label = "NotInitalized";
                Name = "NotInitilized";
            }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("parent")]
            public int ParentId { get; set; }

            [JsonProperty("supporting_nodes")]
            public IList<Node> SupportingNodes { get; set; }
        }

        public class AssuranceCase
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("arguments")]
            public IList<Node> Nodes { get; set; }
        }

        public class ArgumentationStructure
        {
            [JsonProperty("assurance_case")]
            public AssuranceCase AssuranceCase { get; set; }

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

                string type = (string)jo["type"];

                Node item;
                type = type.ToLower().Trim();
                if (type == "claim")
                    item = new Claim();
                else if (type == "strategy")
                    item = new Strategy();
                else if (type == "evidence")
                    item = new Evidence();
                else if (type == "justification")
                    item = new Justification();
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

    namespace ArgumentationV4
    {
        public class Claim : Node
        {

        }

        public class Strategy : Node
        {

        }

        public class Justification : Node
        {

        }

        public class Evidence : Node
        {
            [JsonProperty("filename")]
            public string Filename { get; set; }
        }

        public abstract class Node
        {
            public Node()
            {
                Id = -2;
                Type = "NotInitialized";
                Label = "NotInitalized";
                Name = "NotInitilized";
            }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class Relation
        {
            [JsonProperty("node_supported")]
            public int NodeSupported { get; set; }

            [JsonProperty("supporting_node")]
            public int SupportingNode { get; set; }
        }

        public class AssuranceCase
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("arguments")]
            public IList<Node> Nodes { get; set; }

            [JsonProperty("relations")]
            public IList<Relation> Relations { get; set; }
        }

        public class ArgumentationStructure
        {
            [JsonProperty("assurance_case")]
            public AssuranceCase AssuranceCase { get; set; }

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

                string type = (string)jo["type"];

                Node item;
                type = type.ToLower().Trim();
                if (type == "claim")
                    item = new Claim();
                else if (type == "strategy")
                    item = new Strategy();
                else if (type == "evidence")
                    item = new Evidence();
                else if (type == "justification")
                    item = new Justification();
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

