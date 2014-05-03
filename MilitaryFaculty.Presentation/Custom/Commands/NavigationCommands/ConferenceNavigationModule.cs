using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Commands;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ConferenceNavigationModule : BaseNavigationModule
    {
        public ConferenceNavigationModule(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        public override void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("container");
            }

            commands.AddCommand<Professor>(Browse.ConferenceAdd,
                                           OnBrowseConferenceAdd);

            commands.AddCommand<Conference>(Browse.ConferenceDetails,
                                            OnBrowseConferenceDetails);
        }

        private void OnBrowseConferenceAdd(Professor curator)
        {
            if (curator == null)
            {
                throw new ArgumentNullException("curator");
            }

            var model = new Conference {Curator = curator};
            ViewModel.WorkWindow = new AddConferenceViewModel(model);
        }

        private void OnBrowseConferenceDetails(Conference conference)
        {
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }

            ViewModel.WorkWindow = new ConferenceRootViewModel(conference);
        }
    }
}