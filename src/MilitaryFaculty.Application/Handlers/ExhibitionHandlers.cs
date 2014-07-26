using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class ExhibitionHandlers : EntityCommandModule<Exhibition>
    {
        public ExhibitionHandlers(IRepository<Exhibition> repository)
            : base(repository)
        {
            // Empty
        }
     
        protected override string GetRemovalMessage(Exhibition exhibition)
        {
            return ("Вы действительно хотите удалить информацию об участии в выставке научных работ? " +
                    "Все данные будут утеряны.");
        }
    }

    public class ExhibitionNavigation : NavigationCommandModule
    {
        public ExhibitionNavigation(MainViewModel viewModel)
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

            commands.AddCommand<Professor>(GlobalCommands.BrowseAdd<Exhibition>(), OnBrowseExhibitionAdd);
            commands.AddCommand<Exhibition>(GlobalCommands.BrowseDetails<Exhibition>(), OnBrowseExhibitionDetails);
        }

        private void OnBrowseExhibitionAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Exhibition { Participant = author };
            ViewModel.WorkWindow = new ExhibitionView.Add(model);
        }

        private void OnBrowseExhibitionDetails(Exhibition publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("publication");
            }

            ViewModel.WorkWindow = new ExhibitionView.Root(publication);
        }
    }
}