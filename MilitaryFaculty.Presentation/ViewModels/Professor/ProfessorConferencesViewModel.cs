using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorConferencesViewModel : ViewModel<Professor>
    {
        private ObservableCollection<ConferenceListItemViewModel> _conferences;

        public override string Title
        {
            get { return "Участие в конференциях"; }
        }

        public ObservableCollection<ConferenceListItemViewModel> Conferences
        {
            get
            {
                if (_conferences == null)
                {
                    InitConferences();
                }

                return _conferences;
            }
        }

        public int ConferencesCount
        {
            get { return Conferences.Count; }
        }

        public ProfessorConferencesViewModel(Professor model, IRepository<Conference> conferenceRepository)
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

            _conferences = new ObservableCollection<ConferenceListItemViewModel>(items);
            _conferences.CollectionChanged += (sender, args) => { OnPropertyChanged("ConferencesCount"); };
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
    }
}