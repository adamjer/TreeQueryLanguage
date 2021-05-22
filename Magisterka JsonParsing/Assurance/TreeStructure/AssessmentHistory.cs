using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
    public class AssessmentHistory
    {
        [JsonProperty("assessment")]
        private List<Assessment> _assessments { get; set; }

        public Assessment At(int index)
        {
            try
            {
                return this._assessments.ElementAt(index);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.Out.WriteLine(e.Message);
            }

            throw new Exception("Error: Exception in function At() in class AssessmentHistory");
        }
    }
}
