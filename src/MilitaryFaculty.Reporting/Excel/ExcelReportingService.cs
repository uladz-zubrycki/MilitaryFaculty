using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MilitaryFaculty.Common;
using OfficeOpenXml;

namespace MilitaryFaculty.Reporting.Excel
{
    public class ExcelReportingService : IExcelReportingService
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
                            .ForEach(table => ExportTable(ws, table, reportObject.Names));

                xlPackage.Save();
            }
        }

        public void ExportReport(string filePath, ICollection<Report> reportObjects)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (reportObjects == null)
            {
                throw new ArgumentNullException("reportObjects");
            }
            if (reportObjects.Count == 0)
            {
                throw new ArgumentException("reportObject");
            }

            var curObject = Report.Unify(reportObjects);

            ExportReport(filePath, curObject);
        }

        private void ExportTable(ExcelWorksheet workSheet, ReportTable table, ICollection<string> names)
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
            var xlWriter = new ExcelWriter(workSheet, firstLine, firstColumn, names.Count);

            xlWriter.PutTableHead(table.Name, names);
            
            ICollection<int> totalRating = null;
            foreach (var group in table.ReportGroups)
            {
                var groupResults = GenerateGroup(xlWriter, group);
                totalRating = totalRating == null ? groupResults : SummarizeResults(totalRating, groupResults);
            }

            xlWriter.PutTableResults(totalRating);
            xlWriter.SetGlobalStyles();
        }

        private ICollection<int> GenerateGroup(ExcelWriter xlWriter, ReportGroup group)
        {
            ICollection<int> results = null;

            xlWriter.PutGroupHead(group.Name);

            foreach (var reporRow in group.ReportRows)
            {
                var rowResult = GenerateRow(xlWriter, reporRow);
                results = results == null ? rowResult : SummarizeResults(results, rowResult);
            }

            xlWriter.PutGroupResults(results);

            return results;
        }

        private ICollection<int> GenerateRow(ExcelWriter xlWriter, ReportRow reportRow)
        {
            xlWriter.PutResultsRow(reportRow.Name, reportRow.MaxValue, reportRow.Results);
            return reportRow.Results;
        }

        private ICollection<int> SummarizeResults(ICollection<int> first, ICollection<int> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }
            if (second == null)
            {
                throw new ArgumentNullException("second");
            }
            if (first.Count != second.Count)
            {
                throw new ArgumentException("Length doesn't match");
            }

            var result = first.ToList();

            for (int i = 0; i < result.Count; i++)
            {
                result[i] += second.ElementAt(i);
            }

            return result;
        }
    }
}