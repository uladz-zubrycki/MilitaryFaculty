using System;
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

            commands.AddCommand<Professor>(GlobalCommands.BrowseAdd<CouncilParticipation>(),
                                           OnBrowseAdd);

            commands.AddCommand<CouncilParticipation>(GlobalCommands.BrowseDetails<CouncilParticipation>(),
                                                     OnBrowseDetails);
        }

        private void OnBrowseAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Research { Author = author };
            ViewModel.WorkWindow = new ResearchView.Add(model);
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