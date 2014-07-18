using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting.Data.DataProviders;

namespace MilitaryFaculty.Reporting.Data
{
    //TODO: Need to refactor this class, too much hardcode!
    public class DataProvidersContainer
    {
        private readonly AcademicDegreeChangingsDataProvider _academicDegreeChangings;
        private readonly BooksDataProvider _books;
        private readonly CathedrasDataProvider _cathedras;
        private readonly ConferencesDataProvider _conferences;
        private readonly ExhibitionsDataProvider _exhibitions;
        private readonly ImprovementSuggestionsDataProvider _improvementSuggestions;
        private readonly ParticipationsDataProvider _participations;
        private readonly ProfessorsDataProvider _professors;
        private readonly PublicationsDataProvider _publications;
        private readonly ScientificRequestsDataProvider _scientificRequests;
        private readonly ScientificResearchesDataProvider _scientificResearches;
        private readonly SynopsesDataProvider _synopses;

        public DataProvidersContainer(
            AcademicDegreeChangingsDataProvider academicDegreeChanging,
            BooksDataProvider books,
            CathedrasDataProvider cathedras,
            ConferencesDataProvider conferences,
            ExhibitionsDataProvider exhibitions,
            ImprovementSuggestionsDataProvider improvementSuggestions,
            ParticipationsDataProvider participationsDataProvider,
            ProfessorsDataProvider professors,
            PublicationsDataProvider publications,
            ScientificRequestsDataProvider scientificRequests,
            ScientificResearchesDataProvider scientificResearches,
            SynopsesDataProvider synopses
            )
        {
            #region ParametersCheching

            if (academicDegreeChanging == null)
            {
                throw new ArgumentNullException("academicDegreeChanging");
            }
            if (books == null)
            {
                throw new ArgumentNullException("books");
            }
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
            if (improvementSuggestions == null)
            {
                throw new ArgumentNullException("improvementSuggestions");
            }
            if (participationsDataProvider == null)
            {
                throw new ArgumentNullException("participationsDataProvider");
            }
            if (professors == null)
            {
                throw new ArgumentNullException("professors");
            }
            if (publications == null)
            {
                throw new ArgumentNullException("publications");
            }
            if (scientificRequests == null)
            {
                throw new ArgumentNullException("scientificRequests");
            }
            if (scientificResearches == null)
            {
                throw new ArgumentNullException("scientificResearches");
            }
            if (synopses == null)
            {
                throw new ArgumentNullException("synopses");
            }

            #endregion //ParametersCheching

            _academicDegreeChangings = academicDegreeChanging;
            _books = books;
            _cathedras = cathedras;
            _conferences = conferences;
            _exhibitions = exhibitions;
            _improvementSuggestions = improvementSuggestions;
            _participations = participationsDataProvider;
            _professors = professors;
            _publications = publications;
            _scientificRequests = scientificRequests;
            _scientificResearches = scientificResearches;
            _synopses = synopses;
        }

        public void ClearModificators()
        {
            _academicDegreeChangings.QueryModificator = null;
            _books.QueryModificator = null;
            _cathedras.QueryModificator = null;
            _conferences.QueryModificator = null;
            _exhibitions.QueryModificator = null;
            _improvementSuggestions.QueryModificator = null;
            _participations.QueryModificator = null;
            _professors.QueryModificator = null;
            _publications.QueryModificator = null;
            _scientificRequests.QueryModificator = null;
            _scientificResearches.QueryModificator = null;
            _synopses.QueryModificator = null;
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
            _academicDegreeChangings.QueryModificator = adc =>
                adc.Target.Id == professor.Id
                && adc.Date >= interval.From
                && adc.Date <= interval.To;
            _books.QueryModificator = b =>
                b.Author.Id == professor.Id
                && b.Date >= interval.From
                && b.Date <= interval.To;
            _cathedras.QueryModificator = null;
            _conferences.QueryModificator = con =>
                con.Curator.Id == professor.Id
                && con.Date >= interval.From
                && con.Date <= interval.To;
            _exhibitions.QueryModificator = exh =>
                exh.Participant.Id == professor.Id
                && exh.Date >= interval.From
                && exh.Date <= interval.To;
            _improvementSuggestions.QueryModificator = imsug =>
                imsug.Author.Id == professor.Id
                && imsug.Date >= interval.From
                && imsug.Date <= interval.To;
            _participations.QueryModificator = part =>
                part.Participant.Id == professor.Id
                && ((part.StartDate >= interval.From && part.StartDate <= interval.To)
                || (part.EndDate >= interval.From && part.StartDate <= interval.To)
                || (part.StartDate <= interval.From && part.EndDate >= interval.To));
            _professors.QueryModificator = prof =>
                //TODO: Professors interval (with prof contract)
                prof.Id == professor.Id;
            _publications.QueryModificator = pub =>
                pub.Author.Id == professor.Id
                && pub.Date >= interval.From
                && pub.Date <= interval.To;
            _scientificRequests.QueryModificator = srq =>
                srq.Author.Id == professor.Id
                && srq.Date >= interval.From
                && srq.Date <= interval.To;
            _scientificResearches.QueryModificator = srs =>
                srs.Author.Id == professor.Id
                && srs.Date >= interval.From
                && srs.Date <= interval.To;
            _synopses.QueryModificator = syn =>
                syn.Author.Id == professor.Id
                && syn.Date >= interval.From
                && syn.Date <= interval.To;
        }

        public IEnumerable<IDataProvider> GetProviders()
        {
            var providers = new IDataProvider[]
            {
                _books,
                _cathedras,
                _conferences,
                _exhibitions,
                _improvementSuggestions,
                _professors,
                _publications,
                _scientificRequests,
                _scientificResearches,
                _synopses
            };

            return providers;
        }
    }
}