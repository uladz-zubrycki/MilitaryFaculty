using System;
using System.Collections.Generic;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorRootViewModel : EntityRootViewModel<Professor>
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Conference> _conferenceRepository;
        private readonly IRepository<Exhibition> _exhibitionRepository;
        private readonly IRepository<Publication> _publicationRepository;

        public ProfessorRootViewModel(Professor model,
                                      IRepository<Conference> conferenceRepository,
                                      IRepository<Publication> publicationRepository,
                                      IRepository<Exhibition> exhibitionRepository,
                                      IRepository<Book> bookRepository)
            : base(model)
        {
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            if (publicationRepository == null)
            {
                throw new ArgumentNullException("booksRepository");
            }

            if (exhibitionRepository == null)
            {
                throw new ArgumentNullException("exhibitionRepository");
            }

            if (bookRepository == null)
            {
                throw new ArgumentNullException("bookRepository");
            }

            _conferenceRepository = conferenceRepository;
            _publicationRepository = publicationRepository;
            _exhibitionRepository = exhibitionRepository;
            _bookRepository = bookRepository;

            HeaderViewModel = new ProfessorHeaderViewModel(Model);
        }

        protected override IEnumerable<ViewModel<Professor>> GetViewModels()
        {
            return new ViewModel<Professor>[]
                   {
                       new ProfessorExtraInfoViewModel(Model),
                       new ProfessorConferencesViewModel(Model, _conferenceRepository),
                       new ProfessorPublicationsViewModel(Model, _publicationRepository),
                       new ProfessorExhibitionsViewModel(Model, _exhibitionRepository),
                       new ProfessorBooksViewModel(Model, _bookRepository)
                   };
        }
    }
}