using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Magisterka_JsonParsing.TreeStructure;
using Microsoft.SqlServer;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Magisterka_JsonParsing
{
    class Program
    {
        

        static void Main(string[] args)
        {
            try
            {
                string file = Path.GetFullPath("Argumentations/argument1-v1.json");
                using (StreamReader sr = new StreamReader(file))
                {
                    string jsonContent = sr.ReadToEnd();
                    ArgumentationV1.ArgumentationStructure assuranceCase = JsonConvert.DeserializeObject<ArgumentationV1.ArgumentationStructure>(jsonContent, new ArgumentationV1.ArgumentsConverter());
                    AssuranceCaseTreeView assuranceCaseTreeView = new AssuranceCaseTreeView(assuranceCase.AssuranceCase);

                    var x = assuranceCaseTreeView.Arguments.Descendants().
                        Where(node => node is TreeStructure.Evidence).
                        Where(node => node.Ascendants().Any(n => n.Id == 1 && n is TreeStructure.Claim));
                }

                file = Path.GetFullPath("Argumentations/argument1-v2.json");
                using (StreamReader sr = new StreamReader(file))
                {
                    string jsonContent = sr.ReadToEnd();
                    ArgumentationV2.ArgumentationStructure assuranceCase = JsonConvert.DeserializeObject<ArgumentationV2.ArgumentationStructure>(jsonContent, new ArgumentationV2.ArgumentsConverter());
                    AssuranceCaseTreeView assuranceCaseTreeView = new AssuranceCaseTreeView(assuranceCase.AssuranceCase);
                }

                file = Path.GetFullPath("Argumentations/argument1-v3.json");
                using (StreamReader sr = new StreamReader(file))
                {
                    string jsonContent = sr.ReadToEnd();
                    ArgumentationV3.ArgumentationStructure assuranceCase = JsonConvert.DeserializeObject<ArgumentationV3.ArgumentationStructure>(jsonContent, new ArgumentationV3.ArgumentsConverter());
                }

                file = Path.GetFullPath("Argumentations/argument1-v4.json");
                using (StreamReader sr = new StreamReader(file))
                {
                    string jsonContent = sr.ReadToEnd();
                    ArgumentationV4.ArgumentationStructure assuranceCase = JsonConvert.DeserializeObject<ArgumentationV4.ArgumentationStructure>(jsonContent, new ArgumentationV4.ArgumentsConverter());
                }

                file = Path.GetFullPath("Argumentations/Device safety - XML report.xml");
                //file = Path.GetFullPath("Argumentations/test.json");
                XmlDocument doc = new XmlDocument();
                using (StreamReader sr = new StreamReader(file))
                {
                    string xmlRaw = sr.ReadToEnd();
                    xmlRaw = xmlRaw.Replace(@"<report>", @"<report xmlns:json='http://james.newtonking.com/projects/json'>");
                    xmlRaw = xmlRaw.ReplaceFirst(@"<node ", @"<root ");
                    xmlRaw = xmlRaw.Replace(@"<node ", @"<node json:Array='true' ");
                    xmlRaw = xmlRaw.Replace(@"<section ", @"<section json:Array='true' ");
                    xmlRaw = xmlRaw.Replace(@"<sectionElement ", @"<sectionElement json:Array='true' ");
                    xmlRaw = xmlRaw.Replace(@"<assessment isChange=" , @"<assessment json:Array='true' isChange=");
                    xmlRaw = xmlRaw.ReplaceFirst(@"<root ", @"<node ");
                    doc.LoadXml(xmlRaw);

                    string json = JsonConvert.SerializeXmlNode(doc);

                    ArgumentationFromXML.AssuranceCase report = JsonConvert.DeserializeObject<ArgumentationFromXML.AssuranceCase>(json, new ArgumentationFromXML.ArgumentsConverter());
                    report.Report.Project.Init();

                    ArgumentationFromXML.Root Arguments = new ArgumentationFromXML.Root();
                    Arguments.Children.Add(report.Report.Project.Root);

                    //1) daj elementy argumentacji, które mają pusty opis oceny
                    var x1 = Arguments.Descendants().Where(n => n.Assessment != null).Where(n => n.Assessment.Comment == "");
                    //zwraca 8 elementów
                    var x2 = Arguments.Descendants().Where(n => n.Assessment != null).Where(n => n.Assessment.Decision.Text == "");
                    //zwraca pustą listę
                    var x3 = Arguments.Descendants().Where(n => n.Assessment != null).Where(n => n.Assessment.Confidence.Text == "");
                    //zwraca pustą listę

                    //2) daj dowody (evidence) dla faktów, które są w pełni zaakceptowane
                    //    dla XML: w ocenie znacznik <decision> ma parametr value mniejszy niż 1
                    var y = Arguments.Descendants()
                        .Where(n => n.Assessment is not null && Double.TryParse(n.Assessment.Decision.Value, out _) && Double.Parse(n.Assessment.Decision.Value) < 1)
                        .Where(n => n is ArgumentationFromXML.Claim && n.Parent is ArgumentationFromXML.Strategy);
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

        //IEnumerable<TokenInfo> ParseSql(string sql)
        //{
        //    ParseOptions parseOptions = new ParseOptions();
        //    Scanner scanner = new Scanner(parseOptions);

        //    int state = 0,
        //        start,
        //        end,
        //        lastTokenEnd = -1,
        //        token;

        //    bool isPairMatch, isExecAutoParamHelp;

        //    List<TokenInfo> tokens = new List<TokenInfo>();

        //    scanner.SetSource(sql, 0);

        //    while ((token = scanner.GetNext(ref state, out start, out end, out isPairMatch, out isExecAutoParamHelp)) != (int)Tokens.EOF)
        //    {
        //        TokenInfo tokenInfo =
        //            new TokenInfo()
        //            {
        //                Start = start,
        //                End = end,
        //                IsPairMatch = isPairMatch,
        //                IsExecAutoParamHelp = isExecAutoParamHelp,
        //                Sql = sql.Substring(start, end - start + 1),
        //                Token = (Tokens)token,
        //            };

        //        tokens.Add(tokenInfo);

        //        lastTokenEnd = end;
        //    }

        //    return tokens;
        //}
    }
}
