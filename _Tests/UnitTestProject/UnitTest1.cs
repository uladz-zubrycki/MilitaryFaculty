using System;
using System.Linq;
using NUnit.Framework;
using VNO.DataAccess;
using VNO.Domain;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void CathedraRepositoryTest()
        {
            const string expected = "Cathedra";
            var c = new Cathedra(expected);
            var cr = new CathedraRepository();
            cr.Create(c);
            var actual = cr.Select(x => x.Id == c.Id).First().Name;

            Assert.AreEqual(expected, actual);

            cr.Delete(c.Id);
        }
    }
}