using System;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.ReportObjectDomain;

namespace MilitaryFaculty.Reporting.Excel
{
    public class FacultyExcelReportingService : SingleInstanceExcelService
    {
        public FacultyExcelReportingService
            (
                Func<ReportType, IReportTableProvider> reportTableProviderFactory,
                IFormulaProvider formulaProvider,
                ReportDataProvider reportDataProvider
            )
            : base
            (
                new ReportObject
                (
                    reportTableProviderFactory(ReportType.Faculty),
                    formulaProvider,
                    reportDataProvider
                )
            )
        {
            //Nothing to do here
        }
    }
}