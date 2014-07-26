using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class PublicationHandlers : EntityCommandModule<Publication>
    {
        public PublicationHandlers(IRepository<Publication> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(Publication publication)
        {
            return ("Вы действительно хотите удалить публикацию? " +
                    "Все данные будут утеряны.");
        }
    }

    public class PublicationNavigation : NavigationCommandModule
    {
        public PublicationNavigation(MainViewModel viewModel)
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

            commands.AddCommand<Professor>(GlobalCommands.BrowseAdd<Publication>(), OnBrowsePublicationAdd);
            commands.AddCommand<Publication>(GlobalCommands.BrowseDetails<Publication>(), OnBrowsePublicationDetails);
        }

        private void OnBrowsePublicationAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Publication { Author = author };
            ViewModel.WorkWindow = new PublicationView.Add(model);
        }

        private void OnBrowsePublicationDetails(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("publication");
            }

            ViewModel.WorkWindow = new PublicationView.Root(publication);
        }
    }
}