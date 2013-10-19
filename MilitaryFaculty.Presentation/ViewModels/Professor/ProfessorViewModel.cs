using System;
using MilitaryFaculty.DataAccess.Contract;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorViewModel : ComplexViewModel<Professor>
    {
        #region Class Fields

        private readonly IConferenceRepository conferenceRepository;
        private readonly IBookRepository bookRepository;

        #endregion // Class Fields

        #region Class Properties

        public ProfessorInfoViewModel InfoViewModel { get; private set; }
        public ProfessorExtraInfoViewModel ExtraInfoViewModel { get; private set; }
        public ProfessorConferencesViewModel ConferencesViewModel { get; private set; }
        public ProfessorBooksViewModel BooksViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorViewModel(Professor model,
                                  IConferenceRepository conferenceRepository,
                                  IBookRepository bookRepository)
            : base(model)
        {
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }
            
            if (bookRepository == null)
            {
                throw new ArgumentNullException("booksRepository");
            }

            this.conferenceRepository = conferenceRepository;
            this.bookRepository = bookRepository;

            InitViewModels();
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private void InitViewModels()
        {
            InfoViewModel = new ProfessorInfoViewModel(Model);
            ExtraInfoViewModel = new ProfessorExtraInfoViewModel(Model);
            ConferencesViewModel = new ProfessorConferencesViewModel(Model, conferenceRepository);
            BooksViewModel = new ProfessorBooksViewModel(Model, bookRepository);

            ViewModels.Add(ExtraInfoViewModel);
            ViewModels.Add(ConferencesViewModel);
            ViewModels.Add(BooksViewModel);
        }
        
        #endregion // Class Private Methods
    }
}