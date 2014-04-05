using MilitaryFaculty.Reporting.ReportDomain;

namespace MilitaryFaculty.Reporting
{
    public interface IReportGenerator
    {
        Report Generate(object entity, TimeInterval interval);
    }
}
