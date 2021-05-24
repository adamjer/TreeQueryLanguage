﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
    public class Target
    {
        [JsonProperty("@id")]
        public String ID { get; set; }
        [JsonProperty("@type")]
        public String Type { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("label")]
        public String Label { get; set; }

        public Target()
        {

        }

        public Target(Node node)
        {
            this.ID = node.ID;
            this.Type = node.GetType().Name;
            this.Name = node.Name;
            this.Label = node.Label;
        }

        public static bool operator ==(Target t1, Target t2)
        {
            if ((object)t1 == null)
                return (object)t2 == null;

            return t1.Equals(t2);
        }

        public static bool operator !=(Target b1, Target b2)
        {
            return !(b1 == b2);
        }

        public bool Equals(Target other)
        {
            if (this.ID == other.ID)
                return true;
            return false;
        }

        public static bool operator ==(Target t1, Node t2)
        {
            if ((object)t1 == null)
                return (object)t2 == null;

            return t1.Equals(t2);
        }

        public static bool operator !=(Target b1, Node b2)
        {
            return !(b1 == b2);
        }

        public bool Equals(Node node)
        {
            if (this.ID == node.ID)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is Target)
                return this.Equals((Target)obj);
            else if (obj is Node)
                return this.Equals((Node)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode() ^ this.Type.GetHashCode() ^ this.Name.GetHashCode() ^ this.Label.GetHashCode();
        }

    }

    public class Link : Node
    {
        public Link()
        {
            this.Target = new Target();
        }

        public override IEnumerable<Node> Descendants()
        {
            yield break;
            //duplicated descendants
            //for (int i = 0; i < this.Children.Count; i++)
            //{
            //    
            //    yield return this.Children.At(i);
            //}
        }

        public void ChangeTarget(Node node)
        {
            this.Target = new Target(node);
            this.Children.Clear(node);
        }
    }
}
