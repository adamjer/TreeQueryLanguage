using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
    public class AssessmentInHistory : Assessment
    {
        [JsonProperty("@isChange")]
        public String IsChange { get; set; }
        [JsonProperty("@snapshot")]
        public String Snapshot { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class AssessmentHistory
    {
        [JsonProperty("assessment")]
        private List<AssessmentInHistory> _assessments { get; set; }

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
