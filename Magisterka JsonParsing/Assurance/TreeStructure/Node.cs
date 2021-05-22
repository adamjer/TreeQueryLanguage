using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
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
            this.Assessment = new Assessment();
            this.AssessmentHistory = new AssessmentHistory();
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

        public IEnumerable<Node> Supported(Root root)
        {
            for (int i = 0; i < this.Children.Count; i++)
            {
                var child = this.Children.At(i);
                if (child is Link)
                {
                    yield return root.Descendants().Where(n => n.ID == child.ID).FirstOrDefault();
                }
                else
                {
                    yield return child;
                }
            }
        }
    }
}
