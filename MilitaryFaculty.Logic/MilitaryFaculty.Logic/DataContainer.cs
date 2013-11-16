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

            double totalRating = 0;
            double totalMaxRating = 0;

            var xlRange = xlWorkSheet.Range["b" + curLine, "e" + (curLine + 1)];
            XlStyle.SetNameStyle(xlRange, tableInfo.Name);

            curLine += 2;

            foreach (var part in tableInfo.TableParts)
            {
                xlRange = xlWorkSheet.Range["b" + curLine, "e" + curLine];
                XlStyle.SetSubNameStyle(xlRange, part.Name);
                curLine++;

                foreach (var id in part.Identifiers)
                {
                    var formulaInfo = tableFormulas.Formulas.First(f => f.Id == id).ToFormulaInfo();
                    var characteristic = new Characteristic(formulaInfo);
                    var val = characteristic.Evaluate();

                    xlWorkSheet.Cells[curLine, 3] = formulaInfo.Name;
                    xlWorkSheet.Cells[curLine, 4] = val.ToString("G0");
                    xlWorkSheet.Cells[curLine, 5] = Math.Abs(formulaInfo.MaxValue - 0) < 0.001
                                                        ? "-"
                                                        : formulaInfo.MaxValue.ToString("G0");
                    totalRating += val;
                    totalMaxRating += formulaInfo.MaxValue;
                    curLine++;
                }
            }

            XlStyle.SetNameStyle(xlWorkSheet.Range["b" + curLine, "c" + curLine], "Итог");
            xlWorkSheet.Cells[curLine, 4] = totalRating.ToString("G0");
            xlWorkSheet.Cells[curLine, 5] = totalMaxRating.ToString("G0");

            XlStyle.SetTableStyle(xlWorkSheet.Range["b" + firstLine, "e" + (curLine)]);
            XlStyle.SetSheetStyle(xlWorkSheet, 3, 4, 5);
        }

        #endregion // Class Public Methods
    }
}
