using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Extensions;
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

            ViewModels.AddRange(new ViewModel<Professor>[]
                                {
                                    new ProfessorExtraInfoViewModel(Model),
                                    new ProfessorConferencesViewModel(Model, conferenceRepository),
                                    new ProfessorPublicationsViewModel(Model, publicationRepository)
                                });
        }
        
        #endregion // Class Private Methods
    }
}