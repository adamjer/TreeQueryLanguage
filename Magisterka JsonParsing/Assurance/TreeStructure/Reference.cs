using System;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
    public class Reference : Node
    {
        public String AutoUpdate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? MaxvalidityPeriod { get; set; }
    }
}
