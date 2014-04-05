namespace MilitaryFaculty.Reporting.ReportDomain
{
    public class ReportRow
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public int MaxValue { get; private set; }

        public ReportRow(string name, int value, int maxValue)
        {
            Name = name;
            Value = value;
            MaxValue = maxValue;
        }
    }
}