using System;
using System.Collections.Generic;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorRootViewModel : EntityRootViewModel<Professor>
    {
        private readonly IConferenceRepository conferenceRepository;
        private readonly IPublicationRepository publicationRepository;

        public ProfessorRootViewModel(Professor model,
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

            HeaderViewModel = new ProfessorHeaderViewModel(Model);
        }

        protected override IEnumerable<ViewModel<Professor>> GetViewModels()
        {
            return new ViewModel<Professor>[]
                   {
                       new ProfessorExtraInfoViewModel(Model),
                       new ProfessorConferencesViewModel(Model, conferenceRepository),
                       new ProfessorPublicationsViewModel(Model, publicationRepository)
                   };
        }
    }
}