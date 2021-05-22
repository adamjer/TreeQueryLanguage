using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka_JsonParsing.Assurance.TreeStructure
{
    public class Reference : Node
    {
        public String RepositoryName { get; set; }
        public String Repository { get; set; }
        public String Address { get; set; }
        public String AutoUpdate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? MaxvalidityPeriod { get; set; }
    }
}
