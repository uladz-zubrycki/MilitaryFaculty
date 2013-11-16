using MilitaryFaculty.Domain;
using NUnit.Framework;

namespace MilitaryFaculty.Logic.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    class DataProvider_Test
    {
        private string atrName;

        [SetUp]
        public void SetUp()
        {
            atrName = "PlanDocsOrg";
        }

        [Test]
        public void TestXml()
        {
            var dataProvider = new DataProvider(new Cathedra());
            var i = dataProvider.GetValue(atrName);
        }
    }
    // ReSharper restore InconsistentNaming
}
