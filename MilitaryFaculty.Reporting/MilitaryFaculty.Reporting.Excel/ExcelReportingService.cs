using MilitaryFaculty.Extensions;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.XmlDomain;
using OfficeOpenXml;
using System;
using System.IO;

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
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            File.Delete(filePath);
            var newFile = new FileInfo(filePath);

            using (var xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets.Add("Resuts");

                tableProvider.GetTables()
                         .ForEach(table => ExportTable(table, ws));

                xlPackage.Save();
            }
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void ExportTable(XReportTable table, ExcelWorksheet workSheet)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }

            var xlHelper = new XlResultsHelper(2);

            var firstLine = workSheet.Dimension != null ?  workSheet.Dimension.End.Row + 2 : 2;
            var curLine = firstLine;
            double totalRating = 0;

            xlHelper.PutName(table.Name, workSheet, ref curLine);

            //Groups generation
            foreach (var part in table.Groups)
            {
                xlHelper.PutSubName(part.Name, workSheet, ref curLine);

                //Formula lines generation
                foreach (var formulaId in part.Formulas)
                {
                    var formulaInfo = formulaProvider.GetFormula(formulaId);
                    var characteristic = new Characteristic(formulaInfo, reportDataProvider);
                    var value = NormalizeValue(characteristic.Evaluate());

                    //Fields setting
                    xlHelper.PutFieldLine(formulaInfo.Name, value.ToString("F0"), ValueOrDash(formulaInfo.MaxValue), workSheet, ref curLine);
                    totalRating += value;
                }
            }

            //Results setting
            xlHelper.PutResults("Итог", totalRating.ToString("F0"), workSheet, ref curLine);

            //Sheet styles setting
            xlHelper.SetTableStyle(firstLine, curLine - 1, workSheet);
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

        #endregion // Class Private Methods
    }
}