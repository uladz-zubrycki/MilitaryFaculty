using System;
using System.IO;
using Microsoft.Office.Interop.Excel;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting.Excel
{
    public class ExcelReportingService
    {
        #region Class Fields

        private readonly IReportTableProvider tableProvider;
        private readonly IFormulaProvider formulaProvider;
        private readonly ReportDataProvider reportDataProvider;

        #endregion // Class Fields

        #region Class Constructors

        public ExcelReportingService(IReportTableProvider tableProvider, IFormulaProvider formulaProvider,
                                     ReportDataProvider reportDataProvider)
        {
            if (tableProvider == null)
            {
                throw new ArgumentNullException("tableProvider");
            }
            if (formulaProvider == null)
            {
                throw new ArgumentNullException("formulaProvider");
            }
            if (reportDataProvider == null)
            {
                throw new ArgumentNullException("reportDataProvider");
            }

            this.tableProvider = tableProvider;
            this.formulaProvider = formulaProvider;
            this.reportDataProvider = reportDataProvider;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void ExportReport(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            object misValue = System.Reflection.Missing.Value;

            var xlApp = new Application();
            var xlWorkBook = xlApp.Workbooks.Add(misValue);
            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.Item[1];

            tableProvider.GetTables()
                         .ForEach(table => ExportTable(table, xlWorkSheet));

            File.Delete(filePath);
            xlWorkBook.SaveAs(filePath, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue,
                              misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue,
                              misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void ExportTable(XReportTable table, Worksheet xlWorkSheet)
        {
            if (xlWorkSheet == null)
            {
                throw new ArgumentNullException("xlWorkSheet");
            }

            var firstLine = xlWorkSheet.UsedRange.Rows.Count + 1;
            var curLine = firstLine;
            double totalRating = 0;

            var xlRange = xlWorkSheet.Range["b" + curLine, "e" + (curLine + 1)];
            XlStyle.SetNameStyle(xlRange, table.Name);

            curLine += 2;

            //Groups generation
            foreach (var part in table.Groups)
            {
                xlRange = xlWorkSheet.Range["b" + curLine, "e" + curLine];
                XlStyle.SetSubNameStyle(xlRange, part.Name);
                curLine++;

                //Formula lines generation
                foreach (var formulaId in part.Formulas)
                {
                    var formulaInfo = formulaProvider.GetFormula(formulaId);
                    var characteristic = new Characteristic(formulaInfo, reportDataProvider);
                    var value = NormalizeValue(characteristic.Evaluate());

                    //Fields setting
                    xlWorkSheet.Cells[curLine, 3] = formulaInfo.Name;
                    xlWorkSheet.Cells[curLine, 4] = value.ToString("F0");
                    xlWorkSheet.Cells[curLine, 5] = ValueOrDash(formulaInfo.MaxValue);

                    totalRating += value;
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
 
        private static double NormalizeValue(double value)
        {
            return double.IsInfinity(value) ? 0 : value;
        }

        private static string ValueOrDash(double value)
        {
            return Math.Abs(value) < 0.001
                       ? "-"
                       : value.ToString("F0");
        }

        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion // Class Private Methods
    }
}