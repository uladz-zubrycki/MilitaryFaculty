using System;
using MilitaryFaculty.Reporting.Data;

namespace MilitaryFaculty.Reporting.ReportObjectDomain
{
	public class FacultyReportObjectFactory : ReportObjectFactory
	{
		public FacultyReportObjectFactory
			(
				Func<ReportType, IReportTableProvider> reportTableProviderFactory,
				IFormulaProvider formulaProvider,
				ReportDataProvider reportDataProvider
			)
			: base
				(
					reportTableProviderFactory(ReportType.Faculty),
					formulaProvider,
					reportDataProvider
				)
		{
		}

		public override ReportObject CreateReportObject()
		{
			return new ReportObject(_tableProvider, _formulaProvider, _reportDataProvider);
		}
	}
}
