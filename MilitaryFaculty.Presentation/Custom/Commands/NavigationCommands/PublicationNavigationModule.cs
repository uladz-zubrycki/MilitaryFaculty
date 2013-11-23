using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class PublicationNavigationModule : BaseNavigationModule
    {
        #region Class Constructors

        public PublicationNavigationModule(MainViewModel viewModel)
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

            container.RegisterCommand<Professor>(Browse.Publication.Add, OnBrowseBookAdd);
            container.RegisterCommand<Publication>(Browse.Publication.Details, OnBrowseBookDetails);
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