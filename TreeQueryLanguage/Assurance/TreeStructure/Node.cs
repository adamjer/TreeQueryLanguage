using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeQueryLanguage.Assurance.TreeStructure
{
    public abstract class Node
    {
        [JsonProperty("@id")]
        public String ID { get; set; }
        //[JsonProperty("@type")]
        //public String Type { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("label")]
        public String Label { get; set; }
        [JsonProperty("@tags")]
        public Tags Tags { get; set; }
        [JsonProperty("description")]
        public String Description { get; set; }
        //Only Reference
        [JsonProperty("repositoryName")]
        public String RepositoryName { get; set; }
        //Only Reference
        [JsonProperty("repository")]
        public String Repository { get; set; }
        //Only Reference
        [JsonProperty("address")]
        public String Address { get; set; }
        //Only Rationale
        [JsonProperty("aggregationRule")]
        public String AggregationRule { get; set; }
        //Only [Claim, Fact, Assumption]
        [JsonProperty("weight")]
        public String Weight { get; set; }
        //Only Strategy
        [JsonProperty("counterArgumentation")]
        public String CounterArgumentation { get; set; }
        //Only Link
        [JsonProperty("target")]
        public Target Target { get; set; }
        //[JsonProperty("binding")]
        //public Object Binding { get; set; }
        [JsonProperty("assessment")]
        //Only [Claim, Strategy, Assumption, Fact, Rationale, conditionally Link]
        public Assessment Assessment { get; set; }
        //Only [Claim, Strategy, Assumption, Fact, Rationale, conditionally Link]
        [JsonProperty("assessmentHistory")]
        public AssessmentHistory? AssessmentHistory { get; set; }
        [JsonProperty("nodes")]
        public Nodes Children { get; set; }

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

        public bool IsRoot()
        {
            return this.Parent == null;
        }

        public bool IsLeaf()
        {
            return this.Children.Count == 0;
        }

        public void Add(Node node)
        {
            this.Children.Add(node);
        }

        public void AddRange(IEnumerable<Node> nodes)
        {
            this.Children.AddRange(nodes);
        }

        public virtual IEnumerable<Node> Descendants()
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

        public bool Contains<T>()
        {
            return this.Children.Contains<T>();
        }
    }
}
