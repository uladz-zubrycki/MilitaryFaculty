using System;
using System.Collections.ObjectModel;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorConferencesViewModel : ViewModel<Professor>
    {
        private readonly Lazy<ObservableCollection<ConferenceListItemViewModel>> _conferences;

        public ProfessorConferencesViewModel(Professor model, IRepository<Conference> conferenceRepository)
            : base(model)
        {
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            _conferences = Lazy.Create(CreateConferencesViewModel);

            conferenceRepository.EntityCreated += OnConferenceCreated;
            conferenceRepository.EntityDeleted += OnConferenceDeleted;

            Commands.Add(CreateAddConferenceCommand());
        }

        public ObservableCollection<ConferenceListItemViewModel> Conferences
        {
            get { return _conferences.Value; }
        }

        public override string Title
        {
            get { return "Участие в конференциях"; }
        }

        public int ConferencesCount
        {
            get { return Conferences.Count; }
        }

        private ImagedCommandViewModel CreateAddConferenceCommand()
        {
            const string tooltip = "Добавить конференцию";
            const string imageSource = @"..\Content\add.png";

            return new ImagedCommandViewModel(Browse.ConferenceAdd,
                                              Model, tooltip, imageSource);
        }

        private ObservableCollection<ConferenceListItemViewModel> CreateConferencesViewModel()
        {
            var conferences = Model.Conferences
                                   .Select(ConferenceListItemViewModel.FromModel)
                                   .ToList();

            var result = new ObservableCollection<ConferenceListItemViewModel>(conferences);
            result.CollectionChanged += (sender, args) => OnPropertyChanged("ConferencesCount");

            return result;
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