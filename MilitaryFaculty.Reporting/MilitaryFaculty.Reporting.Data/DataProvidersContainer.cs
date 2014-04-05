using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting.Data.DataProviders;

namespace MilitaryFaculty.Reporting.Data
{
    public class DataProvidersContainer
    {
        private readonly CathedrasDataProvider _cathedras;
        private readonly ConferencesDataProvider _conferences;
        private readonly ExhibitionsDataProvider _exhibitions;
        private readonly ProfessorsDataProvider _professors;
        private readonly PublicationsDataProvider _publications;

        public DataProvidersContainer(
            CathedrasDataProvider cathedras,
            ConferencesDataProvider conferences,
            ExhibitionsDataProvider exhibitions,
            ProfessorsDataProvider professors,
            PublicationsDataProvider publications
            )
        {
            if (cathedras == null)
            {
                throw new ArgumentNullException("cathedras");
            }
            if (conferences == null)
            {
                throw new ArgumentNullException("conferences");
            }
            if (exhibitions == null)
            {
                throw new ArgumentNullException("exhibitions");
            }
            if (professors == null)
            {
                throw new ArgumentNullException("professors");
            }
            if (publications == null)
            {
                throw new ArgumentNullException("publications");
            }

            _cathedras = cathedras;
            _conferences = conferences;
            _exhibitions = exhibitions;
            _professors = professors;
            _publications = publications;
        }

        public void ClearModificators()
        {
            _cathedras.QueryModificator = null;
            _conferences.QueryModificator = null;
            _exhibitions.QueryModificator = null;
            _professors.QueryModificator = null;
            _publications.QueryModificator = null;
        }

        public void SetProfessorModificator(Professor professor)
        {
            _cathedras.QueryModificator = null;
            _conferences.QueryModificator = con => con.Curator.Id == professor.Id;
            _exhibitions.QueryModificator = exh => exh.Participant.Id == professor.Id;
            _professors.QueryModificator = prof => prof.Id == professor.Id;
            _publications.QueryModificator = pub => pub.Author.Id == professor.Id;
        }

        public IEnumerable<IDataProvider> GetProviders()
        {
            var providers = new IDataProvider[]
            {
                _cathedras,
                _conferences,
                _exhibitions,
                _professors,
                _publications
            };

            return providers;
        }
    }
}