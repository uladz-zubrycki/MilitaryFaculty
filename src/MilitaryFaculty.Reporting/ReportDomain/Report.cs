using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Providers;

namespace MilitaryFaculty.Reporting.ReportDomain
{
    public class Report
    {
        public ICollection<string> Names { get; private set; } 
        public ICollection<ReportTable> ReportTables { get; private set; }

        public string Name
        {
            get { return Names.First(); }
        }

        private Report() {}

        public Report(string name,
                      IReportTableProvider tableProvider,
                      IFormulaProvider formulaProvider,
                      ReportDataProvider reportDataProvider)
        {
            if (tableProvider == null)
            {
                throw new ArgumentNullException("tableProvider");
            }
            if (formulaProvider == null)
            {
                throw new ArgumentNullException("formulaProvider");
            }
            if (reportDataProvider == null)
            {
                throw new ArgumentNullException("reportDataProvider");
            }

            Names = new List<string> {name};
            ReportTables = new List<ReportTable>();

            foreach (var xmlTable in tableProvider.GetTables())
            {
                var reportTable = new ReportTable(xmlTable, formulaProvider, reportDataProvider);
                ReportTables.Add(reportTable);
            }
        }

        public static Report Unify(ICollection<Report> reports)
        {
            if (reports == null || reports.Count == 0)
            {
                throw new ArgumentException("reports");
            }

            var newReport = new Report
            {
                Names = UnifyNames(reports),
                ReportTables = new List<ReportTable>()
            };

            for (var i = 0; i < reports.First().ReportTables.Count; i++)
            {
                var collection = GetTablesCollection(reports, i);
                newReport.ReportTables.Add(ReportTable.Unify(collection));
            }

            return newReport;
        }

        private static List<string> UnifyNames(ICollection<Report> reports)
        {
            var names = new List<string>();
            for (var i = 0; i < reports.Count; i++)
            {
                names.AddRange(reports.ElementAt(i).Names);
            }

            return names;
        }

        private static ICollection<ReportTable> GetTablesCollection(ICollection<Report> reports, int tableNumber)
        {
            var tablesCollection = new List<ReportTable>();
            for (var i = 0; i < reports.Count; i++)
            {
                tablesCollection.Add(reports.ElementAt(i).ReportTables.ElementAt(tableNumber));
            }

            return tablesCollection;
        }
    }
}