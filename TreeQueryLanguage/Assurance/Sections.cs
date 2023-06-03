using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeQueryLanguage.Assurance
{
    public class Sections
    {
        [JsonProperty("section")]
        private List<Section> Section { get; set; }

        public Section At(int index)
        {
            try
            {
                return this.Section.ElementAt(index);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.Out.WriteLine(e.Message);
            }

            throw new Exception("Error: Exception in function At() in class Sections");
        }
    }
}
