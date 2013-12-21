using System;
using System.Collections.Generic;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorRootViewModel : EntityRootViewModel<Professor>
    {
        private readonly IRepository<Conference> conferenceRepository;
        private readonly IRepository<Publication> publicationRepository;
        private IRepository<Exhibition> exhibitionRepository;
        private IRepository<Book> bookRepository;

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

            this.conferenceRepository = conferenceRepository;
            this.publicationRepository = publicationRepository;
            this.exhibitionRepository = exhibitionRepository;
            this.bookRepository = bookRepository;

            HeaderViewModel = new ProfessorHeaderViewModel(Model);
        }

        protected override IEnumerable<ViewModel<Professor>> GetViewModels()
        {
            return new ViewModel<Professor>[]
                   {
                       new ProfessorExtraInfoViewModel(Model),
                       new ProfessorConferencesViewModel(Model, conferenceRepository),
                       new ProfessorPublicationsViewModel(Model, publicationRepository),
                       new ProfessorExhibitionsViewModel(Model, exhibitionRepository),
                       new ProfessorBooksViewModel(Model, bookRepository)
                   };
        }
    }
}