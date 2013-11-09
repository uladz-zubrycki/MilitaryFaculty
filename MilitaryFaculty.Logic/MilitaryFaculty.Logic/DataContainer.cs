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
            //Initialization part

            var tableInfo = TableInfo.Deserialize(infoPath);
            var tableFormulas = TableFormulas.Deserialize(formulasPath);

            var firstLine = xlWorkSheet.UsedRange.Rows.Count + 1;
            var curLine = firstLine;

            //Data filling part

            var chartRange = xlWorkSheet.Range["b" + curLine, "d" + (curLine + 1)];
            chartRange.Merge(false);
            chartRange.FormulaR1C1 = tableInfo.Name;
            chartRange.Font.Bold = true;
            chartRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            curLine += 2;

            foreach (var part in tableInfo.TableParts)
            {
                chartRange = xlWorkSheet.Range["b" + curLine, "d" + curLine];
                chartRange.Merge(false);
                chartRange.FormulaR1C1 = part.Name;
                chartRange.Font.Bold = true;

                curLine += 1;

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

            //Table design part

            chartRange = xlWorkSheet.Range["b" + firstLine, "d" + (curLine - 1)];
            chartRange.BorderAround(XlLineStyle.xlDouble, XlBorderWeight.xlThick,
                                    XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
            chartRange.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;

            chartRange.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            chartRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
            chartRange.WrapText = true;

            ((Range) xlWorkSheet.Columns[3, Type.Missing]).EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) xlWorkSheet.Columns[3, Type.Missing]).EntireColumn.ColumnWidth = 70; // ~500px
        }

        #endregion // Class Public Methods
    }
}
