using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting
{
    public interface IReportGenerator
    {
        Report.Report GenerateFacultyReport(TimeInterval interval);
        Report.Report GenerateCathedraReport(Cathedra cathedra, TimeInterval interval);
        Report.Report GenerateProfessorReport(Professor professor, TimeInterval interval);
    }
}