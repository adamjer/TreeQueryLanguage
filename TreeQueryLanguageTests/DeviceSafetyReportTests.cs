using Newtonsoft.Json;
using System.Xml;
using TreeQueryLanguage;
using TreeQueryLanguage.Assurance;
using TreeQueryLanguage.Assurance.TreeStructure;

namespace TreeQueryLanguageTests
{
    [TestClass]
    public class DeviceSafetyReportTests
    {
        public TestContext? TestContext { get; set; }
        public static AssuranceCase? Report { get; set; }

        private Root? Arguments { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            string file = Path.GetFullPath("Argumentations/Device safety - XML report.xml");
            XmlDocument doc = new();
            using (StreamReader sr = new(file))
            {
                string xmlRaw = sr.ReadToEnd();
                doc.LoadXml(Helpers.PrepareXML(xmlRaw));

                string json = JsonConvert.SerializeXmlNode(doc);

                Report = JsonConvert.DeserializeObject<AssuranceCase>(json, new ArgumentsConverter());
                Assert.IsNotNull(Report);
                Report.Init();
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Arguments = new Root();
            Assert.IsNotNull(Report);
            Arguments.Add(Report.Root);
        }


        [TestMethod]
        public void BasicArgumentationCheckout()
        {
            Assert.IsNotNull(Arguments);
            var descendants = Arguments.Descendants();

            Assert.AreEqual(17, descendants.Count());
            var groups = descendants.GroupBy(x => x.GetType());
            Assert.AreEqual(7, groups.Count());

            var emptyComment = descendants.Where(n => n.Assessment.Comment == "");
            Assert.AreEqual(9, emptyComment.Count());
            var nullComment = descendants.Where(n => n.Assessment.Comment == null);
            Assert.AreEqual(6, nullComment.Count());
            var nullOrEmptyComment = descendants.Where(n => String.IsNullOrEmpty(n.Assessment.Comment));
            Assert.AreEqual(emptyComment.Count() + nullComment.Count(), nullOrEmptyComment.Count());
            var someComment = descendants.Where(n => !String.IsNullOrEmpty(n.Assessment.Comment));
            Assert.AreEqual(2, someComment.Count());


        }
    }
}