using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
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

        public override void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.RegisterCommand<Professor>(Browse.Conference.Add,
                OnBrowseConferenceAdd);

            container.RegisterCommand<Conference>(Browse.Conference.Details,
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