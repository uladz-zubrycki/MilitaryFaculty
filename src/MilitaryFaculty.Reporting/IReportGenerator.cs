using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting
{
    public interface IReportGenerator
    {
        Report GenerateFacultyReport(TimeInterval interval);
        Report GenerateCathedraReport(Cathedra cathedra, TimeInterval interval);
        Report GenerateProfessorReport(Professor professor, TimeInterval interval);
    }
}