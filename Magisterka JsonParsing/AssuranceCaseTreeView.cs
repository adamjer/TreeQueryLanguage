using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Magisterka_JsonParsing
{
    namespace TreeStructure
    {
        public class Root : Node
        {
            public Root()
            {
                this.SupportedNodes = new List<Node>();
                this.SupportingNode = null;
                this.Id = 0;
                this.Label = "Root";
                this.Name = "Root";
            }

            public Root(ArgumentationV1.Node node) :
                base(node)
            {

            }

            public Root(ArgumentationV2.Node node) :
                base(node)
            {

            }
        }

        public class Claim : Node
        {
            public Claim(ArgumentationV1.Node node) :
                base(node)
            {

            }

            public Claim(ArgumentationV2.Node node) :
                base(node)
            {

            }
        }

        public class Strategy : Node
        {
            public Strategy(ArgumentationV1.Node node) :
                base(node)
            {

            }

            public Strategy(ArgumentationV2.Node node) :
                base(node)
            {

            }
        }

        public class Justification : Node
        {
            public Justification(ArgumentationV1.Node node) :
                base(node)
            {

            }

            public Justification(ArgumentationV2.Node node) :
                base(node)
            {

            }
        }

        public class Evidence : Node
        {
            public string Filename { get; set; }

            public Evidence(ArgumentationV1.Node node) :
                base(node)
            {
                this.Filename = (node as ArgumentationV1.Evidence).Filename;
            }

            public Evidence(ArgumentationV2.Node node) :
                base(node)
            {
                this.Filename = (node as ArgumentationV2.Evidence).Filename;
            }
        }


        public abstract class Node
        {
            public Node SupportingNode { get; set; }
            public int Id { get; set; }
            public string Label { get; set; }
            public string Name { get; set; }
            public IList<Node> SupportedNodes { get; set; }

            public Node()
            {

            }

            public Node(int id)
            {
                this.Id = id;
            }

            public Node(ArgumentationV1.Node node)
            {
                if (node.IsRoot())
                    this.SupportingNode = null;

                this.Id = node.Id;
                this.Label = node.Label;
                this.Name = node.Name;

                this.SupportedNodes = new List<Node>();
            }

            public Node(ArgumentationV2.Node node)
            {
                if (node.IsRoot())
                    this.SupportingNode = null;

                this.Id = node.Id;
                this.Label = node.Label;
                this.Name = node.Name;

                this.SupportedNodes = new List<Node>();
            }

            public bool IsLeaf()
            {
                if (this.SupportedNodes == null || this.SupportedNodes.Count == 0)
                    return true;
                return false;
            }

            public bool IsRoot()
            {
                if (this.SupportingNode == null)
                    return true;
                return false;
            }

            public static Node Create(ArgumentationV1.Node node)
            {
                if (node.GetType() == typeof(ArgumentationV1.Claim))
                    return new Claim(node);
                else if (node.GetType() == typeof(ArgumentationV1.Evidence))
                    return new Evidence(node);
                else if (node.GetType() == typeof(ArgumentationV1.Strategy))
                    return new Strategy(node);
                else if (node.GetType() == typeof(ArgumentationV1.Justification))
                    return new Justification(node);
                else
                    throw new Exception("Type not implemented!");
            }

            public static Node Create(ArgumentationV2.Node node)
            {
                if (node.GetType() == typeof(ArgumentationV2.Claim))
                    return new Claim(node);
                else if (node.GetType() == typeof(ArgumentationV2.Evidence))
                    return new Evidence(node);
                else if (node.GetType() == typeof(ArgumentationV2.Strategy))
                    return new Strategy(node);
                else if (node.GetType() == typeof(ArgumentationV2.Justification))
                    return new Justification(node);
                else
                    throw new Exception("Type not implemented!");
            }

            public IEnumerable<Node> Descendants()
            {
                foreach (var child in this.SupportedNodes)
                {
                    yield return child;

                    foreach (var grandChild in child.Descendants())
                    {
                        yield return grandChild;
                    }
                }
            }

            public IEnumerable<Node> Ascendants()
            {
                var parent = this.SupportingNode;
                while (parent != null)
                {
                    yield return parent;
                    parent = parent.SupportingNode;
                }
            }

            public IEnumerable<Node> Elements()
            {
                foreach (var child in this.SupportedNodes)
                {
                    yield return child;
                }
            }
        }
    }

    public class AssuranceCaseTreeView
    {
        public string Name { get; set; }
        public TreeStructure.Root Arguments { get; set; }


        private TreeStructure.Node GetParent(IList<TreeStructure.Node> nodes, int parentId)
        {
            foreach (TreeStructure.Node node in nodes)
            {
                if (node.Id == parentId)
                    return node;
            }
            throw new Exception($"Parent with id={parentId} not found!");
        }

        public AssuranceCaseTreeView(ArgumentationV1.AssuranceCase assuranceCase)
        {
            this.Name = assuranceCase.Name;
            this.Arguments = new TreeStructure.Root();
            IList<TreeStructure.Node> nodesToGetParents = new List<TreeStructure.Node>();
            foreach (ArgumentationV1.Argument argument in assuranceCase.Arguments)
            {
                foreach (ArgumentationV1.Node node in argument.Nodes)
                {
                    TreeStructure.Node createdNode = TreeStructure.Node.Create(node);
                    nodesToGetParents.Add(createdNode);
                    if (node.IsRoot())
                    {
                        //this.Arguments.Add(createdNode);
                        createdNode.SupportingNode = this.Arguments;
                        this.Arguments.SupportedNodes.Add(createdNode);
                    }
                    else
                    {
                        TreeStructure.Node parent = this.GetParent(nodesToGetParents, node.ParentId);
                        createdNode.SupportingNode = parent;
                        parent.SupportedNodes.Add(createdNode);
                    }
                }
            }
        }

        public AssuranceCaseTreeView(ArgumentationV2.AssuranceCase assuranceCase)
        {
            this.Name = assuranceCase.Name;
            this.Arguments = new TreeStructure.Root();
            IList<TreeStructure.Node> nodesToGetParents = new List<TreeStructure.Node>();
            foreach (ArgumentationV2.Node node in assuranceCase.Nodes)
            {
                TreeStructure.Node createdNode = TreeStructure.Node.Create(node);
                nodesToGetParents.Add(createdNode);
                if (node.IsRoot())
                {
                    //this.Arguments.Add(createdNode);
                    createdNode.SupportingNode = this.Arguments;
                    this.Arguments.SupportedNodes.Add(createdNode);
                }
                else
                {
                    TreeStructure.Node parent = this.GetParent(nodesToGetParents, node.ParentId);
                    createdNode.SupportingNode = parent;
                    parent.SupportedNodes.Add(createdNode);
                }
            }
        }

        public AssuranceCaseTreeView(ArgumentationV3.AssuranceCase assuranceCase)
        {

        }

        public AssuranceCaseTreeView(ArgumentationV4.AssuranceCase assuranceCase)
        {

        }
    }
}
