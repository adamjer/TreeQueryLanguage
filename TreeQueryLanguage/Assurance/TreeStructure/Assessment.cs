using Newtonsoft.Json;
using System;

namespace TreeQueryLanguage.Assurance.TreeStructure
{
#nullable enable
    public class Confidence : IEquatable<Confidence>
    {
        [JsonProperty("@degree")]
        public String? Degree { get; set; }
        [JsonProperty("@maxDegree")]
        public String? MaxDegree { get; set; }
        [JsonProperty("@value")]
        public String? Value { get; set; }
        [JsonProperty("#text")]
        public String? Text { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return this.Equals(obj as Confidence);
        }

        public bool Equals(Confidence other)
        {
            if (other is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return Text == other.Text;
        }

        public bool Equals(String text)
        {
            if (text is null)
            {
                return false;
            }

            return Text == text;
        }

        public override int GetHashCode() => (Degree, MaxDegree, Value, Text).GetHashCode();

        public static bool operator ==(Confidence lhs, Confidence rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Confidence lhs, Confidence rhs) => !(lhs == rhs);

        public static bool operator ==(Confidence lhs, String rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Confidence lhs, String rhs) => !(lhs == rhs);
    }

    public class Decision : IEquatable<Decision>
    {
        [JsonProperty("@degree")]
        public String? Degree { get; set; }
        [JsonProperty("@maxDegree")]
        public String? MaxDegree { get; set; }
        [JsonProperty("@value")]
        public String? Value { get; set; }
        [JsonProperty("#text")]
        public String? Text { get; set; }

        public override bool Equals(object obj) => this.Equals(obj as Decision);

        public bool Equals(Decision other)
        {
            if (other is null)
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

            return Text == other.Text;
        }

        public bool Equals(String text)
        {
            if (text is null)
            {
                return false;
            }

            return Text == text;
        }

        public override int GetHashCode() => (Degree, MaxDegree, Value, Text).GetHashCode();

        public static bool operator ==(Decision lhs, Decision rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Decision lhs, Decision rhs) => !(lhs == rhs);

        public static bool operator ==(Decision lhs, String rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Decision lhs, String rhs) => !(lhs == rhs);
    }

    public class Assessment
    {
        [JsonProperty("confidence")]
        public Confidence? Confidence { get; set; }
        private Confidence? _confidence;
        [JsonProperty("decision")]
        public Decision? Decision { get; set; }
        private Decision? _decision;
        [JsonProperty("comment")]
        public String? Comment { get; set; }
        [JsonProperty("excluded")]
        public String? Excluded { get; set; }
        [JsonProperty("username")]
        public String? UserName { get; set; }
        [JsonProperty("timestamp")]
        public DateTime? TimeStamp { get; set; }


        public Assessment()
        {
            // this.Confidence = new Confidence();
            // this.Decision = new Decision();
        }
    }
}

