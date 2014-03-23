namespace MilitaryFaculty.Reporting.ReportObjectDomain
{
    public class ReportFormulaInfo
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public int MaxValue { get; private set; }

        public ReportFormulaInfo (string name, int value, int maxValue)
        {
            Name = name;
            Value = value;
            MaxValue = maxValue;
        }
    }
}
