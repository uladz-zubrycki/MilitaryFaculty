using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data
{
    public class DataProvidersContainer
    {
        private readonly ICollection<IDataProvider> _dataProviders;

        public DataProvidersContainer(IEnumerable<IDataProvider> providers)
        {
            if (providers == null)
            {
                throw new ArgumentNullException("providers");
            }

            _dataProviders = providers.ToList();
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

        public void SetProfessorModificator(Person professor, TimeInterval interval)
        {
            foreach (var dataProvider in _dataProviders)
            {
                dataProvider.SetPersonModificator(professor, interval);
            }
        }
    }
}