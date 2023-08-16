using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeQueryLanguage
{
    public class Helpers
    {
        public static string PrepareXML(string xml)
        {
            string result = xml.Replace(@"<report>", @"<report xmlns:json='http://james.newtonking.com/projects/json'>")
                .ReplaceFirst(@"<node ", @"<root ")
                .Replace(@"<node ", @"<node json:Array='true' ")
                .Replace(@"<section ", @"<section json:Array='true' ")
                .Replace(@"<sectionElement ", @"<sectionElement json:Array='true' ")
                .Replace(@"<assessment isChange=", @"<assessment json:Array='true' isChange=")
                .ReplaceFirst(@"<root ", @"<node ");

            return result;
        }
    }
}
