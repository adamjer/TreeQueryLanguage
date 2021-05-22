using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magisterka_JsonParsing.Assurance.TreeStructure;

namespace Magisterka_JsonParsing.Assurance
{
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
            else if (type == "link")
                item = new Link();
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
