using System;
using System.Collections.Generic;
using System.IO;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Reporting.ReportDomain;
using OfficeOpenXml;

namespace MilitaryFaculty.Reporting.Excel
{
    public class ExcelReportingService
    {
        public void ExportReport(string filePath, Report reportObject)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (reportObject == null)
            {
                throw new ArgumentNullException("reportObject");
            }

            File.Delete(filePath);
            var newFile = new FileInfo(filePath);

            using (var xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets.Add("Resuts");

                reportObject.ReportTables
                            .ForEach(table => ExportTable(ws, table));

                xlPackage.Save();
            }
        }

        public void ExportReport(string filePath, ICollection<Report> reportObject)
        {
            //if (String.IsNullOrWhiteSpace(filePath))
            //{
            //    throw new ArgumentNullException("filePath");
            //}
            //if (reportObject == null)
            //{
            //    throw new ArgumentNullException("reportObject");
            //}
            //if (reportObject.Count == 0)
            //{
            //    throw new ArgumentException("reportObject");
            //}

            //File.Delete(filePath);
            //var newFile = new FileInfo(filePath);

            //using (var xlPackage = new ExcelPackage(newFile))
            //{
            //    var ws = xlPackage.Workbook.Worksheets.Add("Resuts");

            //    reportObject.FormulasTables
            //                .ForEach(table => ExportTable(ws, table));

            //    xlPackage.Save();
            //}
        }

        private void ExportTable(ExcelWorksheet workSheet, ReportTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }

            const int firstColumn = 2;
            var firstLine = workSheet.Dimension != null ? workSheet.Dimension.End.Row + 2 : 2;
            var xlWriter = new SingleInstanceExcelWriter(workSheet, firstLine, firstColumn);

            var totalRating = 0;

            xlWriter.PutName(table.Name);

            foreach (var group in table.ReportGroups)
            {
                xlWriter.PutSubName(group.Name);

                foreach (var formulaInfo in group.ReportRows)
                {
                    xlWriter.PutFieldLine(formulaInfo.Name,
                        formulaInfo.Value,
                        formulaInfo.MaxValue);

                    totalRating += formulaInfo.Value;
                }
            }

            xlWriter.PutResults("Итог", totalRating);
            xlWriter.SetTableStyle();
        }
    }
}