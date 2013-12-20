using System;
using System.Linq;
using ClosedXML.Excel;
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

        public void GenerateExcelSheet(IXLWorksheet workSheet)
        {
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }

            var firstLine = (workSheet.RangeUsed() == null) ? 2 : workSheet.RangeUsed().RowCount() + 4;

            var curLine = firstLine;
            double totalRating = 0;

            var range = workSheet.Range(curLine, XlStyle.FirstColumn, curLine + 1, XlStyle.LastColumn);
            XlStyle.SetNameStyle(range, tableInfo.Name);

            curLine += 2;

            //Parts generation
            foreach (var part in tableInfo.TableParts)
            {
                range = workSheet.Range(curLine, XlStyle.FirstColumn, curLine, XlStyle.LastColumn);
                XlStyle.SetSubNameStyle(range, part.Name);
                curLine++;

                //Formula lines generation
                foreach (var id in part.Identifiers)
                {
                    var formulaInfo = tableFormulas.Formulas.First(f => f.Id == id).ToFormulaInfo();
                    var characteristic = new Characteristic(formulaInfo, dataModule);
                    var val = NormilizeValue(characteristic.Evaluate());

                    //Fields setting
                    workSheet.Cell(curLine, XlStyle.FirstColumn + 1).Value = formulaInfo.Name;
                    workSheet.Cell(curLine, XlStyle.FirstColumn + 2).Value = val.ToString("F0");
                    workSheet.Cell(curLine, XlStyle.FirstColumn + 3).Value = ValueOrDash(formulaInfo.MaxValue);

                    totalRating += val;
                    curLine++;
                }
            }

            //Results setting
            XlStyle.SetNameStyle(workSheet.Range(curLine, XlStyle.FirstColumn, curLine, XlStyle.FirstColumn + 1), "Итог");
            workSheet.Cell(curLine, XlStyle.FirstColumn + 2).Value = totalRating.ToString("F0");

            //Sheet styles setting
            XlStyle.SetTableStyle(workSheet.Range(firstLine, XlStyle.FirstColumn, curLine, XlStyle.LastColumn));
            XlStyle.SetSheetStyle(workSheet);
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
