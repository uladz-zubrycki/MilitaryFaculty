using System;
using System.Collections.Generic;
using MilitaryFaculty.Data;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorRootViewModel : EntityRootViewModel<Domain.Professor>
    {
        private readonly IRepository<Domain.Book> _bookRepository;
        private readonly IRepository<Domain.Conference> _conferenceRepository;
        private readonly IRepository<Domain.Exhibition> _exhibitionRepository;
        private readonly IRepository<Domain.Publication> _publicationRepository;

        public ProfessorRootViewModel(Domain.Professor model,
                                      IRepository<Domain.Conference> conferenceRepository,
                                      IRepository<Domain.Publication> publicationRepository,
                                      IRepository<Domain.Exhibition> exhibitionRepository,
                                      IRepository<Domain.Book> bookRepository)
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

        protected override IEnumerable<ViewModel<Domain.Professor>> GetViewModels()
        {
            return new ViewModel<Domain.Professor>[]
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