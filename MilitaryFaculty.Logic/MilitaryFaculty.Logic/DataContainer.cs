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

        #region Class Constructors

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

        #endregion // Class Constructors

        #region Class Public Methods

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

            //Parts generation
            foreach (var part in tableInfo.TableParts)
            {
                xlRange = xlWorkSheet.Range["b" + curLine, "e" + curLine];
                XlStyle.SetSubNameStyle(xlRange, part.Name);
                curLine++;

                //Formula lines generation
                foreach (var id in part.Identifiers)
                {
                    var formulaInfo = tableFormulas.Formulas.First(f => f.Id == id).ToFormulaInfo();
                    var characteristic = new Characteristic(formulaInfo, dataModule); 
                    var val = NormilizeValue(characteristic.Evaluate());

                    //Fields setting
                    xlWorkSheet.Cells[curLine, 3] = formulaInfo.Name;
                    xlWorkSheet.Cells[curLine, 4] = val.ToString("F0");
                    xlWorkSheet.Cells[curLine, 5] = ValueOrDash(formulaInfo.MaxValue);

                    totalRating += val;
                    curLine++;
                }
            }

            //Results setting
            XlStyle.SetNameStyle(xlWorkSheet.Range["b" + curLine, "c" + curLine], "Итог");
            xlWorkSheet.Cells[curLine, 4] = totalRating.ToString("F0");

            //Sheet styles setting
            XlStyle.SetTableStyle(xlWorkSheet.Range["b" + firstLine, "e" + (curLine)]);
            XlStyle.SetSheetStyle(xlWorkSheet, 3, 4, 5);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private static double NormilizeValue(double value)
        {
            return double.IsInfinity(value) ? 0 : value;
        }

        private static string ValueOrDash(double value)
        {
            return Math.Abs(value) < 0.001
                       ? "-"
                       : value.ToString("F0");
        }

        #endregion // Class Private Methods
    }
}
