using MilitaryFaculty.Reporting.Data;

namespace MilitaryFaculty.Reporting.ReportObjectDomain
{
	public abstract class ReportObjectFactory
	{
		protected IReportTableProvider _tableProvider;
		protected IFormulaProvider _formulaProvider;
		protected ReportDataProvider _reportDataProvider;

		protected ReportObjectFactory
			(
				IReportTableProvider tableProvider,
				IFormulaProvider formulaProvider,
				ReportDataProvider reportDataProvider
			)
		{
			_tableProvider = tableProvider;
			_formulaProvider = formulaProvider;
			_reportDataProvider = reportDataProvider;
		}

		public abstract ReportObject CreateReportObject();
	}
}