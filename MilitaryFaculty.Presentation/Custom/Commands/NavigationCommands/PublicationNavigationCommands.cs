using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class PublicationNavigationCommands : BaseNavigationCommands
    {
        #region Class Constructors

        public PublicationNavigationCommands(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public override void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("sink");
            }

            container.RegisterCommand<Professor>(NavigationCommands.BrowsePublicationAdd, OnBrowseBookAdd);
            container.RegisterCommand<Publication>(NavigationCommands.BrowsePublicationDetails, OnBrowseBookDetails);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnBrowseBookAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Publication
                        {
                            Author = author
                        };

            ViewModel.WorkWindow = new PublicationAddViewModel(model);
        }

        private void OnBrowseBookDetails(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("publication");
            }

            ViewModel.WorkWindow = new PublicationViewModel(publication);
        }

        #endregion // Class Private Methods
    }
}