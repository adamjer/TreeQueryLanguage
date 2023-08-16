using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TreeQueryLanguage.Assurance.TreeStructure
{
#nullable enable
    public abstract class Node : IEquatable<Node>
    {
        [JsonProperty("@id")]
        public String? ID { get; set; }
        //[JsonProperty("@type")]
        //public String Type { get; set; }
        [JsonProperty("name")]
        public String? Name { get; set; }
        [JsonProperty("label")]
        public String? Label { get; set; }
        [JsonProperty("@tags")]
        public Tags? Tags { get; set; }
        [JsonProperty("description")]
        public String? Description { get; set; }
        //Only Reference
        [JsonProperty("repositoryName")]
        public String? RepositoryName { get; set; }
        //Only Reference
        [JsonProperty("repository")]
        public String? Repository { get; set; }
        //Only Reference
        [JsonProperty("address")]
        public String? Address { get; set; }
        //Only Rationale
        [JsonProperty("aggregationRule")]
        public String? AggregationRule { get; set; }
        //Only [Claim, Fact, Assumption]
        [JsonProperty("weight")]
        public String? Weight { get; set; }
        //Only Strategy
        [JsonProperty("counterArgumentation")]
        public String? CounterArgumentation { get; set; }
        //Only Link
        [JsonProperty("target")]
        public Target? Target { get; set; }
        //[JsonProperty("binding")]
        //public Object Binding { get; set; }
        [JsonProperty("assessment")]
        //Only [Claim, Strategy, Assumption, Fact, Rationale, conditionally Link]
        public Assessment? Assessment { get; set; }
        //Only [Claim, Strategy, Assumption, Fact, Rationale, conditionally Link]
        [JsonProperty("assessmentHistory")]
        public AssessmentHistory? AssessmentHistory { get; set; }
        [JsonProperty("nodes")]
        public Nodes? Children { get; set; }

        public Node? Parent { get; set; }

        //public Node()
        //{
        //    this.Parent = null;
        //    this.ID = null;
        //    this.Label = null;
        //    this.Name = null;
        //    this.Description = null;
        //    this.Assessment = new Assessment();
        //    this.AssessmentHistory = new AssessmentHistory();
        //    this.Children = new Nodes();
        //}

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            return this.Equals(obj as Node);
        }

        public bool Equals(Node? other)
        {
            if (this is null ||other is null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return this.ID == other.ID;
        }

        public override int GetHashCode() => (Parent, ID, Label, Name, Description).GetHashCode();

        public bool IsRoot()
        {
            return this.Parent is null;
        }

        public bool IsLeaf()
        {
            if (this.Children is not null)
                return this.Children.Count == 0;
            return true;
        }

        public void Add(Node? node)
        {
            (this.Children ??= new Nodes()).Add(node);
        }

        public void AddRange(IEnumerable<Node>? nodes)
        {
            (this.Children ??= new Nodes()).AddRange(nodes);
        }

        public virtual IEnumerable<Node> Descendants()
        {
            if (this.Children is not null)
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
            if (this.Children is not null)
            {
                for (int i = 0; i < this.Children.Count; i++)
                {
                    var child = this.Children.At(i);
                    if (child is Link)
                    {
                        yield return root.Descendants().FirstOrDefault(n => n.ID == child.ID);
                    }
                    else
                    {
                        yield return child;
                    }
                }
            }
        }

        public bool Contains<T>()
        {
            if (this.Children is null)
                return false;
            return this.Children.Contains<T>();
        }
    }
}
