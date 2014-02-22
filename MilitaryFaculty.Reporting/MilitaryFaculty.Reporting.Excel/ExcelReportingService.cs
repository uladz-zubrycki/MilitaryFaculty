using System;
using ClosedXML.Excel;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting.Excel
{
    public class ExcelReportingService
    {
        private readonly IFormulaProvider _formulaProvider;
        private readonly ReportDataProvider _reportDataProvider;
        private readonly IReportTableProvider _tableProvider;

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
                throw new ArgumentNullException("ReportDataProvider");
            }

            _tableProvider = tableProvider;
            _formulaProvider = formulaProvider;
            _reportDataProvider = reportDataProvider;
        }

        public void ExportReport(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("workSheet");
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Results");
                _tableProvider.GetTables()
                              .ForEach(table => ExportTable(table, worksheet));
                workbook.SaveAs(filePath);
            }
        }

        private void ExportTable(XReportTable table, IXLWorksheet worksheet)
        {
            var firstLine = (worksheet.RangeUsed() == null) ? 2 : worksheet.RangeUsed().RowCount() + 4;

            var curLine = firstLine;
            double totalRating = 0;

            var range = worksheet.Range(curLine, XlStyle.FirstColumn, curLine + 1, XlStyle.LastColumn);
            XlStyle.SetNameStyle(range, table.Name);

            curLine += 2;

            //Groups generation
            foreach (var part in table.Groups)
            {
                range = worksheet.Range(curLine, XlStyle.FirstColumn, curLine, XlStyle.LastColumn);
                XlStyle.SetSubNameStyle(range, part.Name);
                curLine++;

                //Formula lines generation
                foreach (var formulaId in part.Formulas)
                {
                    var formulaInfo = _formulaProvider.GetFormula(formulaId);
                    var characteristic = new Characteristic(formulaInfo, _reportDataProvider);
                    var value = NormalizeValue(characteristic.Evaluate());

                    //Fields setting
                    worksheet.Cell(curLine, XlStyle.FirstColumn + 1).Value = formulaInfo.Name;
                    worksheet.Cell(curLine, XlStyle.FirstColumn + 2).Value = value.ToString("F0");
                    worksheet.Cell(curLine, XlStyle.FirstColumn + 3).Value = ValueOrDash(formulaInfo.MaxValue);

                    totalRating += value;
                    curLine++;
                }
            }

            //Results setting
            XlStyle.SetNameStyle(worksheet.Range(curLine, XlStyle.FirstColumn, curLine, XlStyle.FirstColumn + 1), "Итог");
            worksheet.Cell(curLine, XlStyle.FirstColumn + 2).Value = totalRating.ToString("F0");

            //Sheet styles setting
            XlStyle.SetTableStyle(worksheet.Range(firstLine, XlStyle.FirstColumn, curLine, XlStyle.LastColumn));
            XlStyle.SetSheetStyle(worksheet);
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
    }
}