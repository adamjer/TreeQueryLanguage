using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
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
            this._nodes = new List<Node>
            {
                node
            };
        }

        public Nodes(List<Node> nodes)
        {
            this._nodes = new List<Node>(nodes.Count);
            for (int i = 0; i < nodes.Count; i++)
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

        public void AddRange(IEnumerable<Node> nodes)
        {
            try
            {
                this._nodes.AddRange(nodes);
                return;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }

            throw new Exception("Error: Exception in function Add() in class Nodes");
        }

        public void Clear()
        {
            this._nodes.Clear();
        }

        public void Clear(Node node)
        {
            this.Clear();
            this.Add(node);
        }

        public bool Contains<T>()
        {
            return this._nodes.OfType<T>().FirstOrDefault() is not null;
        }
    }
}

