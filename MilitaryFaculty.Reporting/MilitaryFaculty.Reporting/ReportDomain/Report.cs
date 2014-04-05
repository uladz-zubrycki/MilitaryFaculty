using System;
using System.Collections.Generic;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Providers;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting.ReportDomain
{
    public class Report
    {
        public string Name { get; private set; }
        public ICollection<ReportTable> FormulasTables { get; private set; }

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

            Name = name;
            FormulasTables = new List<ReportTable>();

            foreach (var xmlTable in tableProvider.GetTables())
            {
                var reportTable = new ReportTable(xmlTable, formulaProvider, reportDataProvider);
                FormulasTables.Add(reportTable);
            }
        }
    }
}