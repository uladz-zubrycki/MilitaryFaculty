using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Structure;
using MilitaryFaculty.Reporting.Structure.XmlDomain;

namespace MilitaryFaculty.Reporting
{
    public class ReportGroup
    {
        public string Name { get; private set; }
        public ICollection<ReportRow> ReportRows { get; private set; }

        private ReportGroup() { }

        public ReportGroup(XReportTableGroup xmlGroup,
            IFormulaProvider formulaProvider,
            ReportDataProvider reportDataProvider)
        {
            if (xmlGroup == null)
            {
                throw new ArgumentNullException("xmlGroup");
            }
            if (formulaProvider == null)
            {
                throw new ArgumentNullException("formulaProvider");
            }
            if (reportDataProvider == null)
            {
                throw new ArgumentNullException("reportDataProvider");
            }

            Name = xmlGroup.Name;
            ReportRows = new List<ReportRow>();

            foreach (var formulaId in xmlGroup.Formulas)
            {
                var formulaInfo = formulaProvider.GetFormula(formulaId);
                var characteristic = new Characteristic(formulaInfo, reportDataProvider);
                var value = NormalizeValue(characteristic.Evaluate());

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                //TODO: Round or Integral part?
                var reportFormula = new ReportRow(formulaInfo.Name,
                    Convert.ToInt32(value),
                    Convert.ToInt32(formulaInfo.MaxValue));
                ReportRows.Add(reportFormula);
            }
        }

        public static ReportGroup Unify(ICollection<ReportGroup> reportGroups)
        {
            if (reportGroups == null || reportGroups.Count == 0)
            {
                throw new ArgumentException("reportGroups");
            }

            CheckNames(reportGroups);

            var newGroup = new ReportGroup
            {
                Name = reportGroups.First().Name,
                ReportRows = new List<ReportRow>()
            };

            for (var i = 0; i < reportGroups.First().ReportRows.Count; i++)
            {
                var collection = GetRowsCollection(reportGroups, i);
                newGroup.ReportRows.Add(ReportRow.Unify(collection));
            }

            return newGroup;
        }

        private static void CheckNames(ICollection<ReportGroup> reportGroups)
        {
            if (reportGroups == null)
            {
                throw new ArgumentNullException("reportGroups");
            }
            if (reportGroups.Any(reportGroup => reportGroup.Name != reportGroups.First().Name))
            {
                throw new Exception("Groups discrepancy");
            }
        }

        private static ICollection<ReportRow> GetRowsCollection(ICollection<ReportGroup> reportGroups, int rowNumber)
        {
            var rowsCollection = new List<ReportRow>();
            for (var i = 0; i < reportGroups.Count; i++)
            {
                rowsCollection.Add(reportGroups.ElementAt(i).ReportRows.ElementAt(rowNumber));
            }

            return rowsCollection;
        }

        private static double NormalizeValue(double value)
        {
            return double.IsInfinity(value) ? 0 : value;
        }
    }
}