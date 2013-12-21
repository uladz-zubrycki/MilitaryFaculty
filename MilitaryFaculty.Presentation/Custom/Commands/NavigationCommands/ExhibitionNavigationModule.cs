using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ExhibitionNavigationModule : BaseNavigationModule
    {
        #region Class Constructors

        public ExhibitionNavigationModule(MainViewModel viewModel)
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

            container.RegisterCommand<Professor>(Browse.Exhibition.Add, OnBrowseExhibitionAdd);
            container.RegisterCommand<Exhibition>(Browse.Exhibition.Details, OnBrowseExhibitionDetails);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnBrowseExhibitionAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Exhibition
                        {
                            Participant = author
                        };

            ViewModel.WorkWindow = new AddExhibitionViewModel(model);
        }

        private void OnBrowseExhibitionDetails(Exhibition publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("publication");
            }

            ViewModel.WorkWindow = new ExhibitionRootViewModel(publication);
        }

        #endregion // Class Private Methods
    }
}