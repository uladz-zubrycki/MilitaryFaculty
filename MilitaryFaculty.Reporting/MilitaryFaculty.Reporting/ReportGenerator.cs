using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Providers;
using MilitaryFaculty.Reporting.ReportDomain;

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

        public virtual Report CreateReportObject()
        {
            return null;
        }

        public Report Generate(object entity, TimeInterval interval)
        {
            if (interval == null)
            {
                throw new ArgumentNullException("interval");
            }

            //For faculty object
            if (entity == null)
            {
                _reportDataProvider.ReportDataProvidersContainer.ClearModificators();
                return new Report("Faculty", _reportTableResolver(typeof (Cathedra)), _formulaProvider,
                    _reportDataProvider);
            }
            if (entity is Cathedra)
            {
                var cathedra = (Cathedra) entity;
                //TODO: Cathedra report
                _reportDataProvider.ReportDataProvidersContainer.ClearModificators();
                return new Report(cathedra.Name, _reportTableResolver(typeof (Cathedra)),
                    _formulaProvider, _reportDataProvider);
            }
            if (entity is Professor)
            {
                var professor = (Professor) entity;
                _reportDataProvider.ReportDataProvidersContainer.SetProfessorModificator(professor, interval);
                return new Report(professor.FullName.ToString(),
                    _reportTableResolver(typeof (Professor)), _formulaProvider, _reportDataProvider);
            }

            throw new ArgumentException("Type of entity is not supported.");
        }
    }
}