using System;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.ReportObjectDomain;

namespace MilitaryFaculty.Reporting.Excel
{
    public class ProfessorExcelReportingService : SingleInstanceExcelService
    {
        public ProfessorExcelReportingService
            (
                Func<ReportType, IReportTableProvider> reportTableProviderFactory,
                IFormulaProvider formulaProvider,
                ReportDataProvider reportDataProvider
            )
            : base
            (
                new ReportObject
                (
                    reportTableProviderFactory(ReportType.Professor),
                    formulaProvider,
                    reportDataProvider
                )
            )
        {
            //Nothing to do here
        }
    }
}
