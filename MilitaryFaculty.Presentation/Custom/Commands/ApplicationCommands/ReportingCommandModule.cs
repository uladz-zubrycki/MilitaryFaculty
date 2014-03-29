using System;
using Autofac.Features.GeneratedFactories;
using Microsoft.Win32;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Reporting.Excel;
using MilitaryFaculty.Reporting.ReportObjectDomain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ReportingCommandModule : ICommandContainerModule
    {
        private readonly ExcelReportingService _service;
	    private readonly ReportObjectFactory _factory;

        public ReportingCommandModule(ExcelReportingService service, ReportObjectFactory factory)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            _service = service;
	        _factory = factory;
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
                _service.ExportReport(filename, _factory.CreateReportObject());
            }
        }
    }
}
