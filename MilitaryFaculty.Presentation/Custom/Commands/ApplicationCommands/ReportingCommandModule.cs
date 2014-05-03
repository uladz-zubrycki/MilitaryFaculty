using System;
using Microsoft.Win32;
using MilitaryFaculty.Presentation.Core.Commands;
using MilitaryFaculty.Reporting.Excel;
using MilitaryFaculty.Reporting.ReportDomain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ReportingCommandModule : ICommandModule
    {
        private readonly ExcelReportingService _service;

        public ReportingCommandModule(ExcelReportingService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            _service = service;
        }

        public void LoadModule(RoutedCommands commands)
        {
            commands.AddCommand(Do.ReportGenerate, GenerateReport);
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
                //TODO: generate report
                Report report = null;
                _service.ExportReport(filename, report);
            }
        }
    }
}