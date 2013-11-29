using System;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using MilitaryFaculty.Logic.XmlDomain;

namespace MilitaryFaculty.Logic
{
    public class DataContainer
    {
        #region Class Fields

        private readonly TableInfo tableInfo;
        private readonly TableFormulas tableFormulas;
        private readonly DataModule dataModule;

        #endregion // Class Fields

        #region Class Public Methods

        public DataContainer(TableInfo tableInfo, TableFormulas tableFormulas, DataModule dataModule)
        {
            if (tableInfo == null)
            {
                throw new ArgumentNullException("tableInfo");
            }
            if (tableFormulas == null)
            {
                throw new ArgumentNullException("tableFormulas");
            }
            if (dataModule == null)
            {
                throw new ArgumentNullException("dataModule");
            }

            this.tableInfo = tableInfo;
            this.tableFormulas = tableFormulas;
            this.dataModule = dataModule;
        }

        public void GenerateExcelSheet(Worksheet xlWorkSheet)
        {
            if (xlWorkSheet == null)
            {
                throw new ArgumentNullException("xlWorkSheet");
            }

            var firstLine = xlWorkSheet.UsedRange.Rows.Count + 1;
            var curLine = firstLine;

            double totalRating = 0;

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
                    var characteristic = new Characteristic(formulaInfo, dataModule); 
                    var val = characteristic.Evaluate();

                    xlWorkSheet.Cells[curLine, 3] = formulaInfo.Name;
                    xlWorkSheet.Cells[curLine, 4] = val.ToString("F0");
                    xlWorkSheet.Cells[curLine, 5] = Math.Abs(formulaInfo.MaxValue) < 0.001
                                                        ? "-"
                                                        : formulaInfo.MaxValue.ToString("F0");
                    totalRating += val;
                    curLine++;
                }
            }

            XlStyle.SetNameStyle(xlWorkSheet.Range["b" + curLine, "c" + curLine], "Итог");
            xlWorkSheet.Cells[curLine, 4] = totalRating.ToString("F0");

            XlStyle.SetTableStyle(xlWorkSheet.Range["b" + firstLine, "e" + (curLine)]);
            XlStyle.SetSheetStyle(xlWorkSheet, 3, 4, 5);
        }

        #endregion // Class Public Methods
    }
}
