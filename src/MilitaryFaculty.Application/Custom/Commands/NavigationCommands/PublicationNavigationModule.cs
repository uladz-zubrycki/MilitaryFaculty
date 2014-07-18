using System;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom
{
    public class PublicationNavigationModule : BaseNavigationModule
    {
        public PublicationNavigationModule(MainViewModel viewModel)
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

            commands.AddCommand<Professor>(Browse.PublicationAdd,
                                           OnBrowsePublicationAdd);

            commands.AddCommand<Publication>(Browse.PublicationDetails,
                                             OnBrowsePublicationDetails);
        }

        private void OnBrowsePublicationAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Publication {Author = author};
            ViewModel.WorkWindow = new AddPublicationViewModel(model);
        }

        private void OnBrowsePublicationDetails(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("publication");
            }

            ViewModel.WorkWindow = new PublicationRootViewModel(publication);
        }
    }
}