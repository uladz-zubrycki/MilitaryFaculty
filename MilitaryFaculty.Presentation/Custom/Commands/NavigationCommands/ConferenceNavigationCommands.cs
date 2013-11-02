using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ConferenceNavigationCommands : BaseNavigationCommands
    {
        #region Class Constructors

        public ConferenceNavigationCommands(MainViewModel viewModel)
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

            container.RegisterCommand<Professor>(NavigationCommands.BrowseConferenceAdd,
                                                 OnBrowseConferenceAdd);

            container.RegisterCommand<Conference>(NavigationCommands.BrowseConferenceDetails,
                                                  OnBrowseConferenceDetails);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnBrowseConferenceAdd(Professor curator)
        {
            if (curator == null)
            {
                throw new ArgumentNullException("curator");
            }

            var model = new Conference
                        {
                            Curator = curator,
                        };

            ViewModel.WorkWindow = new ConferenceAddViewModel(model);
        }

        private void OnBrowseConferenceDetails(Conference conference)
        {
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }

            ViewModel.WorkWindow = new ConferenceViewModel(conference);
        }

        #endregion // Class Private Methods
    }
}