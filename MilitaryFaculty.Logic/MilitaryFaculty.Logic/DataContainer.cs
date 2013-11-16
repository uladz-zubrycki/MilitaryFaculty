using System;
using System.Linq;
using MilitaryFaculty.Logic.XmlFormulasDomain;
using MilitaryFaculty.Logic.XmlInfoDomain;
using Microsoft.Office.Interop.Excel;

namespace MilitaryFaculty.Logic
{

    public class DataContainer
    {
        #region Class Fields

        private readonly string formulasPath;
        private readonly string infoPath;

        #endregion // Class Fields

        #region Class Public Methods

        public DataContainer(string formulasFileName, string infoFileName)
        {
            //AppDomain.CurrentDomain.BaseDirectory
            //var xmlDoc = XDocument.Load("FormulasFourthTable.xml");

            formulasPath = formulasFileName;
            infoPath = infoFileName;
        }

        public void GenerateExcelSheet(Worksheet xlWorkSheet)
        {
            var tableInfo = TableInfo.Deserialize(infoPath);
            var tableFormulas = TableFormulas.Deserialize(formulasPath);

            var firstLine = xlWorkSheet.UsedRange.Rows.Count + 1;
            var curLine = firstLine;

            var xlRange = xlWorkSheet.Range["b" + curLine, "d" + (curLine + 1)];
            XlStyle.SetTableNameStyle(xlRange, tableInfo.Name);

            curLine += 2;

            foreach (var part in tableInfo.TableParts)
            {
                xlRange = xlWorkSheet.Range["b" + curLine, "d" + curLine];
                XlStyle.SetTableSubNameStyle(xlRange, part.Name);
                curLine++;

                foreach (var id in part.Identifiers)
                {
                    var formulaInfo = tableFormulas.Formulas.First(f => f.Id == id).ToFormulaInfo();
                    var characteristic = new Characteristic(formulaInfo);
                    var val = characteristic.Evaluate();

                    xlWorkSheet.Cells[curLine, 3] = formulaInfo.Name;
                    xlWorkSheet.Cells[curLine, 4] = val.ToString("G0");
                    curLine++;
                }
            }

            XlStyle.SetTableStyle(xlWorkSheet.Range["b" + firstLine, "d" + (curLine - 1)]);
            XlStyle.SetSheetStyle(xlWorkSheet, 3);
        }

        #endregion // Class Public Methods
    }
}
