using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Providers;
using MilitaryFaculty.Reporting.ReportDomain;

namespace MilitaryFaculty.Reporting
{
    public class ReportGenerator : IReportGenerator
    {
        private IReportTableResolver ReportTableResolver;
        private IFormulaProvider FormulaProvider;
        private ReportDataProvider ReportDataProvider;

        public ReportGenerator
            (
            IReportTableResolver reportTableResolver,
            IFormulaProvider formulaProvider,
            ReportDataProvider reportDataProvider
            )
        {
            ReportTableResolver = reportTableResolver;
            FormulaProvider = formulaProvider;
            ReportDataProvider = reportDataProvider;
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
                ReportDataProvider.ReportDataProvidersContainer.ClearModificators();
                return new Report("Faculty", ReportTableResolver.GetTableProvider(typeof(Cathedra)), FormulaProvider, ReportDataProvider);
            }
            if (entity is Cathedra)
            {
                var cathedra = (Cathedra) entity;
                //TODO: Cathedra report
                ReportDataProvider.ReportDataProvidersContainer.ClearModificators();
                return new Report(cathedra.Name, ReportTableResolver.GetTableProvider(typeof(Cathedra)), FormulaProvider, ReportDataProvider);
            }
            if (entity is Professor)
            {
                var professor = (Professor) entity;
                ReportDataProvider.ReportDataProvidersContainer.SetProfessorModificator(professor, interval);
                return new Report(professor.FullName.ToString(), ReportTableResolver.GetTableProvider(typeof(Professor)), FormulaProvider, ReportDataProvider);
            }

            throw new ArgumentException("Type of entity is not supported.");
        }
    }
}