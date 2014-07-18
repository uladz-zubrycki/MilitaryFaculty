using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.ReportDomain
{
    public class ReportRow
    {
        public ICollection<int> Results { get; private set; }
        public string Name { get; private set; }
        public int MaxValue { get; private set; }

        public int Value
        {
            get { return Results.First(); }
        }

        private ReportRow() {}

        public ReportRow(string name, int value, int maxValue)
        {
            Name = name;
            Results = new Collection<int> {value};
            MaxValue = maxValue;
        }

        public static ReportRow Unify(ICollection<ReportRow> reportRows)
        {
            if (reportRows == null || reportRows.Count == 0)
            {
                throw new ArgumentException("reportRows");
            }

            var newRow = new ReportRow
            {
                Name = reportRows.First().Name,
                MaxValue = reportRows.First().MaxValue,
                Results = new List<int>()
            };

            for (var i = 0; i < reportRows.Count; i++)
            {
                if (newRow.Name != reportRows.ElementAt(i).Name)
                {
                    throw new Exception("Rows discrepancy");
                }

                newRow.Results.AddRange(reportRows.ElementAt(i).Results);
            }

            return newRow;
        }
    }
}