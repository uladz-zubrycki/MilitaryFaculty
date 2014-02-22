﻿using MilitaryFaculty.Extensions;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.XmlDomain;
using OfficeOpenXml;
using System;
using System.IO;

namespace MilitaryFaculty.Reporting.Excel
{
    public class ExcelReportingService
    {
        private readonly IReportTableProvider _tableProvider;
        private readonly IFormulaProvider _formulaProvider;
        private readonly ReportDataProvider _reportDataProvider;

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

            _tableProvider = tableProvider;
            _formulaProvider = formulaProvider;
            _reportDataProvider = reportDataProvider;
        }

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

                _tableProvider.GetTables()
                         .ForEach(table => ExportTable(table, ws));

                xlPackage.Save();
            }
        }

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

            xlHelper.PutName(workSheet, ref curLine, table.Name);

            //Groups generation
            foreach (var part in table.Groups)
            {
                xlHelper.PutSubName(workSheet, ref curLine, part.Name);

                //Formula lines generation
                foreach (var formulaId in part.Formulas)
                {
                    var formulaInfo = _formulaProvider.GetFormula(formulaId);
                    var characteristic = new Characteristic(formulaInfo, _reportDataProvider);
                    var value = NormalizeValue(characteristic.Evaluate());

                    xlHelper.PutFieldLine(workSheet, ref curLine, formulaInfo.Name, value.ToString("F0"), ValueOrDash(formulaInfo.MaxValue));
                    totalRating += value;
                }
            }

            xlHelper.PutResults(workSheet, ref curLine, "Итог", totalRating.ToString("F0"));

            //Sheet styles setting
            xlHelper.SetTableStyle(workSheet, firstLine, curLine - 1);
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