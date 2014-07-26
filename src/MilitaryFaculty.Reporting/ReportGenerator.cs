using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Structure;

namespace MilitaryFaculty.Reporting
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly Func<Type, IReportTableProvider> _reportTableResolver;
        private readonly IFormulaProvider _formulaProvider;
        private readonly ReportDataProvider _reportDataProvider;

        public ReportGenerator(Func<Type, IReportTableProvider> reportTableResolver,
            IFormulaProvider formulaProvider,
            ReportDataProvider reportDataProvider)
        {
            _reportTableResolver = reportTableResolver;
            _formulaProvider = formulaProvider;
            _reportDataProvider = reportDataProvider;
        }

        public Report GenerateFacultyReport(TimeInterval interval)
        {
            _reportDataProvider.ReportDataProvidersContainer.SetFacultyModificator(interval);
            return new Report("Факультет", _reportTableResolver(typeof (Cathedra)), _formulaProvider,
                _reportDataProvider);
        }

        public Report GenerateCathedraReport(Cathedra cathedra, TimeInterval interval)
        {
            _reportDataProvider.ReportDataProvidersContainer.SetCathedraModificator(cathedra, interval);
            return new Report(cathedra.Name, _reportTableResolver(typeof (Cathedra)), _formulaProvider,
                _reportDataProvider);
        }

        public Report GenerateProfessorReport(Professor professor, TimeInterval interval)
        {
            _reportDataProvider.ReportDataProvidersContainer.SetProfessorModificator(professor, interval);
            return new Report(professor.FullName.ToString(), _reportTableResolver(typeof (Professor)),
                _formulaProvider,
                _reportDataProvider);
        }
    }
}