using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorConferencesViewModel : ViewModel<Professor>
    {
        #region Class Fields

        private ObservableCollection<ConferenceListItemViewModel> conferences;

        #endregion // Class Fields

        #region Class Properties

        public override string Title
        {
            get { return "Участие в конференциях"; }
        }

        public ObservableCollection<ConferenceListItemViewModel> Conferences
        {
            get
            {
                if (conferences == null)
                {
                    InitConferences();
                }

                return conferences;
            }
        }

        public int ConferencesCount
        {
            get { return Conferences.Count; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorConferencesViewModel(Professor model, IConferenceRepository conferenceRepository)
            : base(model)
        {
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            conferenceRepository.EntityCreated += OnConferenceCreated;
            conferenceRepository.EntityDeleted += OnConferenceDeleted;

            Commands.Add(CreateAddConferenceCommand());
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private ImagedCommandViewModel CreateAddConferenceCommand()
        {
            const string tooltip = "Добавить конференцию";
            const string imageSource = @"..\Content\add.png";

            return new ImagedCommandViewModel(Browse.Conference.Add,
                                              Model, tooltip, imageSource);
        }

        private void InitConferences()
        {
            var converter = ConferenceListItemViewModel.FromModel();
            var items = Model.Conferences.Select(converter);

            conferences = new ObservableCollection<ConferenceListItemViewModel>(items);
            conferences.CollectionChanged += (sender, args) => { OnPropertyChanged("ConferencesCount"); };
        }

        private void OnConferenceCreated(object sender, ModifiedEntityEventArgs<Conference> e)
        {
            var conference = e.ModifiedEntity;
            Conferences.Add(new ConferenceListItemViewModel(conference));
        }

        private void OnConferenceDeleted(object sender, ModifiedEntityEventArgs<Conference> e)
        {
            var conference = e.ModifiedEntity;
            Conferences.RemoveSingle(c => c.Model.Equals(conference));
        }

        #endregion // Class Private Methods
    }
}