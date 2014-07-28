using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class ConferenceHandlers : EntityCommandModule<Conference>
    {
        public ConferenceHandlers(IRepository<Conference> repository)
            : base(repository)
        {
            // Empty
        }
      
        protected override string GetRemovalMessage(Conference conference)
        {
            return ("Вы действительно хотите удалить конференцию? " +
                    "Все данные будут утеряны.");
        }
    }

    public class ConferenceNavigation : NavigationCommandModule
    {
        public ConferenceNavigation(MainViewModel viewModel) : base(viewModel) { }

        public override void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("container");
            }

            commands.AddCommand<Person>(GlobalCommands.BrowseAdd<Conference>(), OnBrowseConferenceAdd);
            commands.AddCommand<Conference>(GlobalCommands.BrowseDetails<Conference>(), OnBrowseConferenceDetails);
        }

        private void OnBrowseConferenceAdd(Person curator)
        {
            if (curator == null)
            {
                throw new ArgumentNullException("curator");
            }

            var model = new Conference { Curator = curator };
            ViewModel.WorkWindow = new ConferenceView.Add(model);
        }

        private void OnBrowseConferenceDetails(Conference conference)
        {
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }

            ViewModel.WorkWindow = new ConferenceView.Root(conference);
        }
    }
}