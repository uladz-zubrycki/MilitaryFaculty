using System;
using System.Collections.Generic;
using MilitaryFaculty.Logic.XmlInfoDomain;
using NUnit.Framework;

namespace MilitaryFaculty.Logic.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class TableInfo_Tests
    {
        private TableInfo tableInfo;
        private string path;

        [SetUp]
        public void SetUp()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            //path = "d:\\Other\\git_projects\\MilitaryFaculty\\MilitaryFaculty.Logic\\MilitaryFaculty.Logic.Services\\XmlTables\\";
            path = "d:\\Other\\git\\MilitaryFaculty\\MilitaryFaculty.Logic\\MilitaryFaculty.Logic.Services\\XmlTables\\";
            path += "FirstTableInfo.xml";
        }

        [Test]
        public void TestXml()
        {
            tableInfo = TableInfo.Deserialize(path);
        }
    }
    // ReSharper restore InconsistentNaming
}