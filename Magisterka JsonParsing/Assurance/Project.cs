using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magisterka_JsonParsing.Assurance.TreeStructure;

namespace Magisterka_JsonParsing.Assurance
{
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
        //Interfaces
        
        //Metadata

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
}
