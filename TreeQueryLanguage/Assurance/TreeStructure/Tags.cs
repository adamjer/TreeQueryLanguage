using Newtonsoft.Json;
using System.Collections.Generic;

namespace TreeQueryLanguage.Assurance.TreeStructure
{
    public class Tags
    {
        [JsonProperty("tag")]
        private List<Tag> _tags { get; set; }
        public int Count { get { return _tags.Count; } }

        public Tags()
        {
            this._tags = new List<Tag>();
        }
    }
}
