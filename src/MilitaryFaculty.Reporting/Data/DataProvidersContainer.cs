using System.Collections.Generic;
using System.Collections.ObjectModel;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data
{
    public class DataProvidersContainer
    {
        private readonly ICollection<IDataProvider> _dataProviders;

        public DataProvidersContainer()
        {
            _dataProviders = new Collection<IDataProvider>();
        }

        public void AddDataProvider(IDataProvider dataProvider)
        {
            _dataProviders.Add(dataProvider);
        }

        public IEnumerable<IDataProvider> GetProviders()
        {
            return _dataProviders;
        }
        
        public void SetFacultyModificator(TimeInterval interval)
        {
            foreach (var dataProvider in _dataProviders)
            {
                dataProvider.SetFacultyModificator(interval);
            }
        }

        public void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            foreach (var dataProvider in _dataProviders)
            {
                dataProvider.SetCathedraModificator(cathedra, interval);
            }
        }

        public void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
            foreach (var dataProvider in _dataProviders)
            {
                dataProvider.SetProfessorModificator(professor, interval);
            }
        }
    }
}