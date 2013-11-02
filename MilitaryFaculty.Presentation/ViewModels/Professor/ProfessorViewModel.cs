using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorViewModel : ComplexViewModel<Professor>
    {
        #region Class Fields

        private readonly IConferenceRepository conferenceRepository;
        private readonly IPublicationRepository publicationRepository;

        #endregion // Class Fields

        #region Class Properties

        public ProfessorInfoViewModel InfoViewModel { get; private set; }
        public ProfessorExtraInfoViewModel ExtraInfoViewModel { get; private set; }
        public ProfessorConferencesViewModel ConferencesViewModel { get; private set; }
        public ProfessorPublicationsViewModel BooksViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorViewModel(Professor model,
                                  IConferenceRepository conferenceRepository,
                                  IPublicationRepository publicationRepository)
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

            this.conferenceRepository = conferenceRepository;
            this.publicationRepository = publicationRepository;

            InitViewModels();
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private void InitViewModels()
        {
            InfoViewModel = new ProfessorInfoViewModel(Model);
            ExtraInfoViewModel = new ProfessorExtraInfoViewModel(Model);
            ConferencesViewModel = new ProfessorConferencesViewModel(Model, conferenceRepository);
            BooksViewModel = new ProfessorPublicationsViewModel(Model, publicationRepository);

            ViewModels.Add(ExtraInfoViewModel);
            ViewModels.Add(ConferencesViewModel);
            ViewModels.Add(BooksViewModel);
        }
        
        #endregion // Class Private Methods
    }
}