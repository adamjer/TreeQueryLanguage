using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.SqlServer;
using System.Xml;
using Newtonsoft.Json.Linq;
using TreeQueryLanguage.Assurance;
using TreeQueryLanguage.Assurance.TreeStructure;
using Z.Expressions;
using System.Dynamic;

namespace TreeQueryLanguage
{
    class Program
    {
        static void Main()
        {
            try
            {
                string file = Path.GetFullPath("Argumentations/Device safety - XML report.xml");
                //file = Path.GetFullPath("Argumentations/test.json");
                XmlDocument doc = new();
                using (StreamReader sr = new(file))
                {
                    string xmlRaw = sr.ReadToEnd();
                    doc.LoadXml(PrepareXML(xmlRaw));

                    string json = JsonConvert.SerializeXmlNode(doc);

                    AssuranceCase report = JsonConvert.DeserializeObject<AssuranceCase>(json, new ArgumentsConverter());
                    report.Report.Project.Init();

                    Root Arguments = new();
                    Arguments.Children.Add(report.Report.Project.Root);

                    //1) daj elementy argumentacji, które mają pusty opis oceny
                    var x1 = Arguments.Descendants().Where(n => n.Assessment.Comment == "");
                    //zwraca 8 elementów
                    var x2 = Arguments.Descendants().Where(n => n.Assessment.Decision.Text == "");
                    //zwraca pustą listę
                    var x3 = Arguments.Descendants().Where(n => n.Assessment.Confidence.Text == "");
                    //zwraca pustą listę

                    //2) daj dowody (evidence) dla faktów, które są w pełni zaakceptowane
                    //    dla XML: w ocenie znacznik <decision> ma parametr value mniejszy niż 1
                    var y = Arguments.Descendants()
                        .Where(n => n.Label.ToLower().Contains("evidence"))
                        .Where(n => n.Parent is Fact)
                        .Where(n => Double.TryParse(n.Parent.Assessment.Decision.Value, out _) && Double.Parse(n.Parent.Assessment.Decision.Value) < 1);
                }

                file = Path.GetFullPath("Argumentations/Infusion pump air-in-line hazard assurance case.xml");
                //file = Path.GetFullPath("Argumentations/test.json");
                doc = new XmlDocument();
                using (StreamReader sr = new(file))
                {
                    string xmlRaw = sr.ReadToEnd();
                    doc.LoadXml(PrepareXML(xmlRaw));

                    string json = JsonConvert.SerializeXmlNode(doc);

                    AssuranceCase report = JsonConvert.DeserializeObject<AssuranceCase>(json, new ArgumentsConverter());
                    report.Report.Project.Init();

                    Root Arguments = new();
                    Arguments.Children.Add(report.Report.Project.Root);

                    //1) daj elementy argumentacji, które mają pusty opis oceny
                    var x1 = Arguments.Descendants().Where(n => n.Assessment.Comment == "");
                    //zwraca 8 elementów
                    var x2 = Arguments.Descendants().Where(n => n.Assessment.Decision.Text == "");
                    //zwraca pustą listę
                    var x3 = Arguments.Descendants().Where(n => n.Assessment.Confidence.Text == "");
                    //zwraca pustą listę

                    var facts = Arguments.Descendants()
                        .Where(n => n is Fact)
                        .Where(n => n.Descendants()
                            .Any(m => m.Name.ToLower().Contains("manual"))
                        );


                    dynamic expandoObject = new ExpandoObject();
                    expandoObject.Arguments = Arguments;

                    var result = Eval.Execute<IEnumerable<Node>>(@"
                            Arguments.Descendants().Where(n => n.Name.ToLower().Contains(""manual""));
                        ", expandoObject);


                    Z.Expressions.EvalManager.DefaultContext.RegisterType(typeof(Fact));
                    var result2 = Arguments.Descendants().Execute<IEnumerable<Node>>(@"Where(n => n is Fact)");

                    var result1 = Arguments.Descendants().Execute<IEnumerable<Node>>(
                        @"Where(n => n is Fact)" +
                        @".Where(n => n.Descendants()" +
                            @".Any(m => m.Name.ToLower().Contains(""manual""))" +
                        @")"
                    );

                    //2) daj dowody (evidence) dla faktów, które są w pełni zaakceptowane
                    //    dla XML: w ocenie znacznik <decision> ma parametr value mniejszy niż 1
                    //var y = Arguments.Descendants().OfType<Link>()
                        
                        //.Where(n => n.Parent is Fact);
                        //.Where(n => Double.TryParse(n.Parent.Assessment.Decision.Value, out _) && Double.Parse(n.Parent.Assessment.Decision.Value) < 1);
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

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
