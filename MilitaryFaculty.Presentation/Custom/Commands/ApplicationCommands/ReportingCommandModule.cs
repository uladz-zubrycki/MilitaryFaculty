using System;
using Microsoft.Win32;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Reporting.Excel;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ReportingCommandModule: ICommandContainerModule
    {
        private readonly ExcelReportingService reportingService;

        public ReportingCommandModule(ExcelReportingService reportingService)
        {
            if (reportingService == null)
            {
                throw new ArgumentNullException("ReportingService");
            }

            this.reportingService = reportingService;
        }

        public void RegisterModule(CommandContainer container)
        {
            container.RegisterCommand(Report.Generate, GenerateReport);   
        }

        private void GenerateReport()
        {
            var dialog = new SaveFileDialog
                         {
                             FileName = "Report",
                             DefaultExt = ".xlsx",
                             Filter = "Excel documents (.xlsx)|*.xlsx"
                         };

            if (dialog.ShowDialog() == true)
            {
                var filename = dialog.FileName;
                reportingService.ExportReport(filename);
            }
        }
    }
}
