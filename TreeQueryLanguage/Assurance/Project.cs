using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeQueryLanguage.Assurance.TreeStructure;

namespace TreeQueryLanguage.Assurance
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
            InitLinks(this.Root.Descendants());
        }

        private void InitParents(Node Parent)
        {
            for (int i = 0; i < Parent.Children.Count; i++)
            {
                Parent.Children.At(i).Parent = Parent;
                InitParents(Parent.Children.At(i));
            }
        }

        private static void InitLinks(IEnumerable<Node> descendants)
        {
            foreach (Link link in descendants.OfType<Link>())
            {
                Node target = descendants.Where(n => n.ID == link.Target.ID).FirstOrDefault();
                if (target is null)
                    throw new Exception($"Error: Target.ID={target.ID} not found in descendants!");
                link.Children.Add(target);
            }
        }
    }
}
