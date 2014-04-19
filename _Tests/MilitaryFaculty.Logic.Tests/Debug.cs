using System;
using System.IO;
using MilitaryFaculty.FormulaEditor;
using NUnit.Framework;

namespace MilitaryFaculty.Logic.Tests
{
    public class Debug
    {
        [Test]
        public void DebugMethod()
        {
            var curDir = Environment.CurrentDirectory;
            const string fileName = "formulas-1.xml";
            var filePath = Path.Combine(curDir, "data", fileName);

            var service = new FormulaService();
            var document = service.Load(filePath);
        }
    }
}
