using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class CouncilParticipationHandlers : EntityCommandModule<CouncilParticipation>
    {
        public CouncilParticipationHandlers(IRepository<CouncilParticipation> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(CouncilParticipation publication)
        {
            return ("Вы действительно хотите удалить участие в научном совете? " +
                    "Все данные будут утеряны.");
        }
    }

    public class CouncilParticipationNavigation : NavigationCommandModule
    {
        public CouncilParticipationNavigation(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        public override void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("sink");
            }

            commands.AddCommand<Person>(GlobalCommands.BrowseAdd<CouncilParticipation>(),
                                           OnBrowseAdd);

            commands.AddCommand<CouncilParticipation>(GlobalCommands.BrowseDetails<CouncilParticipation>(),
                                                      OnBrowseDetails);
        }

        private void OnBrowseAdd(Person participant)
        {
            if (participant == null)
            {
                throw new ArgumentNullException("participant");
            }

            var model = new CouncilParticipation { Participant = participant };
            ViewModel.WorkWindow = new CouncilParticipationView.Add(model);
        }

        private void OnBrowseDetails(CouncilParticipation participation)
        {
            if (participation == null)
            {
                throw new ArgumentNullException("participation");
            }

            ViewModel.WorkWindow = new CouncilParticipationView.Root(participation);
        }
    }
}