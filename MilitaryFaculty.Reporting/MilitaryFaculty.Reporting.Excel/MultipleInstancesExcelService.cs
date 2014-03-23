using System;
using System.IO;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.XmlDomain;
using OfficeOpenXml;

namespace MilitaryFaculty.Reporting.Excel
{
    //This code from the old excel service
    //TODO: Implement this class
    public class MultipleInstancesExcelService : IExcelReportingService
    {
        private readonly IReportTableProvider _tableProvider;
        private readonly IFormulaProvider _formulaProvider;
        private readonly ReportDataProvider _reportDataProvider;

        public MultipleInstancesExcelService(
                                                IReportTableProvider tableProvider, 
                                                IFormulaProvider formulaProvider,
                                                ReportDataProvider reportDataProvider
                                            )
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
                    .ForEach(table => ExportTable(ws, table));

                xlPackage.Save();
            }
        }

        private void ExportTable(ExcelWorksheet workSheet, XReportTable table)
        {
            throw new NotImplementedException();
        }
    }
}

