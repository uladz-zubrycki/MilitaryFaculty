using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data
{
    public interface IDataProvider
    {
        void SetFacultyModificator(TimeInterval interval);
        void SetCathedraModificator(Cathedra cathedra, TimeInterval interval);
        void SetPersonModificator(Person professor, TimeInterval interval);
    }
}