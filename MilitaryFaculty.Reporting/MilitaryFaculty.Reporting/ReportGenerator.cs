using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Providers;
using MilitaryFaculty.Reporting.ReportDomain;

namespace MilitaryFaculty.Reporting
{
    public class ReportGenerator : IReportGenerator
    {
        protected IReportTableProvider TableProvider;
        protected IFormulaProvider FormulaProvider;
        protected ReportDataProvider ReportDataProvider;

        public ReportGenerator
            (
            IReportTableProvider tableProvider,
            IFormulaProvider formulaProvider,
            ReportDataProvider reportDataProvider
            )
        {
            TableProvider = tableProvider;
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

            if (entity == null)
            {
                //For faculty object
                ReportDataProvider.ReportDataProvidersContainer.ClearModificators();
                return new Report("Faculty", TableProvider, FormulaProvider, ReportDataProvider);
            }
            if (entity is Cathedra)
            {
                var cathedra = (Cathedra) entity;
                //Temp solution
                ReportDataProvider.ReportDataProvidersContainer.ClearModificators();
                return new Report(cathedra.Name, TableProvider, FormulaProvider, ReportDataProvider);
            }
            if (entity is Professor)
            {
                var professor = (Professor) entity;
                ReportDataProvider.ReportDataProvidersContainer.SetProfessorModificator(professor);
                return new Report(professor.FullName.ToString(), TableProvider, FormulaProvider, ReportDataProvider);
            }

            throw new ArgumentException("Type of entity is not supported.");
        }
    }
}