using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting.Data.DataProviders;

namespace MilitaryFaculty.Reporting.Data
{
    //TODO: Need to refactor this class, too much hardcode!
    public class DataProvidersContainer
    {
        private readonly BooksDataProvider _books;
        private readonly CathedrasDataProvider _cathedras;
        private readonly ConferencesDataProvider _conferences;
        private readonly DissertationWorksDataProvider _dissertationWorks;
        private readonly ExhibitionsDataProvider _exhibitions;
        private readonly ProfessorsDataProvider _professors;
        private readonly PublicationsDataProvider _publications;
        private readonly ScientificExpertisesDataProvider _scientificExpertises;
        private readonly InventiveApplicationsDataProvider _inventiveApplications;
        private readonly ResearchesDataProvider _researches;

        public DataProvidersContainer(
            BooksDataProvider books,
            CathedrasDataProvider cathedras,
            ConferencesDataProvider conferences,
            DissertationWorksDataProvider dissertationWorks,
            ExhibitionsDataProvider exhibitions,
            ProfessorsDataProvider professors,
            PublicationsDataProvider publications,
            ScientificExpertisesDataProvider scientificExpertises,
            InventiveApplicationsDataProvider inventiveApplications,
            ResearchesDataProvider researches
            )
        {
            _books = books;
            _cathedras = cathedras;
            _conferences = conferences;
            _exhibitions = exhibitions;
            _professors = professors;
            _publications = publications;
            _scientificExpertises = scientificExpertises;
            _inventiveApplications = inventiveApplications;
            _researches = researches;
            _dissertationWorks = dissertationWorks;
        }

        public void ClearModificators()
        {
            _books.QueryModificator = null;
            _cathedras.QueryModificator = null;
            _conferences.QueryModificator = null;
            _exhibitions.QueryModificator = null;
            _professors.QueryModificator = null;
            _publications.QueryModificator = null;
            _scientificExpertises.QueryModificator = null;
            _inventiveApplications.QueryModificator = null;
            _researches.QueryModificator = null;
            _dissertationWorks.QueryModificator = null;
        }

        public void SetFacultyModificator(Cathedra cathedra, TimeInterval interval)
        {
            throw new NotImplementedException();
        }

        public void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            throw new NotImplementedException();
        }

        public void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
          
            //_books.QueryModificator = b =>
            //    b.Author.Id == professor.Id
            //    && b.CreatedAt >= interval.From
            //    && b.CreatedAt <= interval.To;
            //_cathedras.QueryModificator = null;
            //_conferences.QueryModificator = con =>
            //    con.Curator.Id == professor.Id
            //    && con.Date >= interval.From
            //    && con.Date <= interval.To;
            //_dissertationWorks.QueryModificator = dw =>
            //    dw.Author.Id == professor.Id
            //    && dw.Date >= interval.From
            //    && dw.Date <= interval.To;
            //_exhibitions.QueryModificator = exh =>
            //    exh.Participant.Id == professor.Id
            //    && exh.Date >= interval.From
            //    && exh.Date <= interval.To;
           
            //_improvementSuggestions.QueryModificator = imsug =>
            //    imsug.Author.Id == professor.Id
            //    && imsug.Date >= interval.From
            //    && imsug.Date <= interval.To;
            //_participations.QueryModificator = part =>
            //    part.Participant.Id == professor.Id
            //    && ((part.StartDate >= interval.From && part.StartDate <= interval.To)
            //        || (part.EndDate >= interval.From && part.EndDate <= interval.To)
            //        || (part.StartDate <= interval.From && part.EndDate >= interval.To));
            //_professors.QueryModificator = prof =>
            //    prof.Id == professor.Id
            //    && ((prof.EnrollDate >= interval.From && prof.EnrollDate <= interval.To)
            //        || (prof.DismissalDate >= interval.From && prof.DismissalDate <= interval.To)
            //        || (prof.EnrollDate <= interval.From && prof.DismissalDate >= interval.To));
            //_publications.QueryModificator = pub =>
            //    pub.Author.Id == professor.Id
            //    && pub.Date >= interval.From
            //    && pub.Date <= interval.To;
            //_scientificExpertises.QueryModificator = se =>
            //    se.Author.Id == professor.Id
            //    && se.Date >= interval.From
            //    && se.Date <= interval.To;
            //_inventiveApplications.QueryModificator = srq =>
            //    srq.Author.Id == professor.Id
            //    && srq.Date >= interval.From
            //    && srq.Date <= interval.To;
            //_researches.QueryModificator = srs =>
            //    srs.Author.Id == professor.Id
            //    && srs.Date >= interval.From
            //    && srs.Date <= interval.To;
        }

        public IEnumerable<IDataProvider> GetProviders()
        {
            var providers = new IDataProvider[]
            {
                _books,
                _cathedras,
                _conferences,
                _exhibitions,
                _professors,
                _publications,
                _scientificExpertises,
                _inventiveApplications,
                _researches,
                _dissertationWorks
            };

            return providers;
        }
    }
}