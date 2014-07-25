using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Structure;
using MilitaryFaculty.Reporting.Structure.XmlDomain;

namespace MilitaryFaculty.Reporting.Report
{
    public class ReportTable
    {
        public string Name { get; private set; }
        public ICollection<ReportGroup> ReportGroups { get; private set; }

        private ReportTable() {}

        public ReportTable(XReportTable xmlTable,
                           IFormulaProvider formulaProvider,
                           ReportDataProvider reportDataProvider)
        {
            if (xmlTable == null)
            {
                throw new ArgumentNullException("xmlTable");
            }
            if (formulaProvider == null)
            {
                throw new ArgumentNullException("formulaProvider");
            }
            if (reportDataProvider == null)
            {
                throw new ArgumentNullException("reportDataProvider");
            }

            Name = xmlTable.Name;
            ReportGroups = new List<ReportGroup>();

            foreach (var xmlGroup in xmlTable.Groups)
            {
                var reportGroup = new ReportGroup(xmlGroup, formulaProvider, reportDataProvider);
                ReportGroups.Add(reportGroup);
            }
        }

        public static ReportTable Unify(ICollection<ReportTable> reportTables)
        {
            if (reportTables == null || reportTables.Count == 0)
            {
                throw new ArgumentException("reportTable");
            }

            CheckNames(reportTables);

            var newTable = new ReportTable
            {
                Name = reportTables.First().Name,
                ReportGroups = new List<ReportGroup>()
            };

            for (var i = 0; i < reportTables.First().ReportGroups.Count; i++)
            {
                var collection = GetGroupsCollection(reportTables, i);
                newTable.ReportGroups.Add(ReportGroup.Unify(collection));
            }

            return newTable;
        }

        private static void CheckNames(ICollection<ReportTable> reportTables)
        {
            if (reportTables.Any(reportTable => reportTable.Name != reportTables.First().Name))
            {
                throw new Exception("Tables discrepancy");
            }
        }

        private static ICollection<ReportGroup> GetGroupsCollection(ICollection<ReportTable> reportTables, int groupNumber)
        {
            var groupsCollection = new List<ReportGroup>();
            for (var i = 0; i < reportTables.Count; i++)
            {
                groupsCollection.Add(reportTables.ElementAt(i).ReportGroups.ElementAt(groupNumber));
            }

            return groupsCollection;
        }
    }
}