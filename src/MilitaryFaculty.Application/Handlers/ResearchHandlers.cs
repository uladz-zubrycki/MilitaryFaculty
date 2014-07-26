using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class ResearchHandlers : EntityCommandModule<Research>
    {
        public ResearchHandlers(IRepository<Research> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(Research publication)
        {
            return ("Вы действительно хотите удалить НИР? " +
                    "Все данные будут утеряны.");
        }
    }

    public class ResearchNavigation : NavigationCommandModule
    {
        public ResearchNavigation(MainViewModel viewModel)
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

            commands.AddCommand<Professor>(GlobalCommands.BrowseAdd<Research>(),
                                           OnBrowseAdd);

            commands.AddCommand<Research>(GlobalCommands.BrowseDetails<Research>(),
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

        private void OnBrowseDetails(Research research)
        {
            if (research == null)
            {
                throw new ArgumentNullException("research");
            }

            ViewModel.WorkWindow = new ResearchView.Root(research);
        }
    }
}