using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Commands;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ExhibitionNavigationModule : BaseNavigationModule
    {
        public ExhibitionNavigationModule(MainViewModel viewModel)
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

            commands.AddCommand<Professor>(Browse.ExhibitionAdd,
                                           OnBrowseExhibitionAdd);

            commands.AddCommand<Exhibition>(Browse.ExhibitionDetails,
                                            OnBrowseExhibitionDetails);
        }

        private void OnBrowseExhibitionAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Exhibition {Participant = author};
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
    }
}