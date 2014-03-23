using System;
using System.Collections.Generic;
using MilitaryFaculty.Reporting.Data;

namespace MilitaryFaculty.Reporting.ReportObjectDomain
{
    public class ReportObject
    {
        public string Name { get; set; }
        public ICollection<ReportTable> FormulasTables { get; private set; }

        public ReportObject(IReportTableProvider tableProvider, 
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

            Name = tableProvider.ToString();
            FormulasTables = new List<ReportTable>();

            foreach (var xmlTable in tableProvider.GetTables())
            {
                var reportTable = new ReportTable(xmlTable, formulaProvider, reportDataProvider);
                FormulasTables.Add(reportTable);
            }
        }

        public ReportObject(string name,
                            IReportTableProvider tableProvider,
                            IFormulaProvider formulaProvider,
                            ReportDataProvider reportDataProvider) 
            : this(tableProvider, formulaProvider, reportDataProvider)
        {
            Name = name;
        }
    }
}
