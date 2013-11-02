// ReSharper disable InconsistentNaming

using System;
using MilitaryFaculty.Logic.XmlFormulasDomain;
using MilitaryFaculty.Logic.XmlInfoDomain;
using NUnit.Framework;

namespace MilitaryFaculty.Logic.Tests
{
    [TestFixture]
    class TableFormulas_Test
    {
        private TableFormulas tableFormulas;
        private string path;

        [SetUp]
        public void SetUp()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            path = "d:\\Other\\git_projects\\MilitaryFaculty\\MilitaryFaculty.Logic\\MilitaryFaculty.Logic.Services\\XmlTables\\";
            path += "FirstTableFormulas.xml";
        }

        [Test]
        public void TestXml()
        {
            tableFormulas = TableFormulas.Deserialize(path);
        }
    }
}
// ReSharper restore InconsistentNaming