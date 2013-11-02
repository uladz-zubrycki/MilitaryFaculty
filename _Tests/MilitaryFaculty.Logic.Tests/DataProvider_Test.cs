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
            atrName = "ProfsCount";
        }

        [Test]
        public void TestXml()
        {
            var i = DataProvider.GetValue(atrName);
        }
    }
    // ReSharper restore InconsistentNaming
}
