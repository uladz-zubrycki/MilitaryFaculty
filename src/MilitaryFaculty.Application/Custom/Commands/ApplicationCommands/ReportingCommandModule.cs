using System;
using Microsoft.Win32;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Excel;

namespace MilitaryFaculty.Application.Custom
{
    public class ReportingCommandModule : ICommandModule
    {
        private readonly IExcelReportingService _service;
        private readonly IReportGenerator _reportGenerator;

        public ReportingCommandModule(IExcelReportingService service, IReportGenerator reportGenerator)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            if (reportGenerator == null)
            {
                throw new ArgumentNullException("reportGenerator");
            }

            _service = service;
            _reportGenerator = reportGenerator;
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

                //TODO: generate true report (add timeinterval and entity)
                var report = _reportGenerator.Generate(null, new TimeInterval(new DateTime(2000, 1, 1), DateTime.Now));

                _service.ExportReport(filename, report);
            }
        }
    }
}