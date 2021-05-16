using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Magisterka_JsonParsing.TreeStructure;
using Microsoft.SqlServer;

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
