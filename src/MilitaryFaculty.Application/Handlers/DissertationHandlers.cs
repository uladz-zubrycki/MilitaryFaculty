using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;
using Dissertation = MilitaryFaculty.Domain.Dissertation;

namespace MilitaryFaculty.Application.Handlers
{
    public class DissertationHandlers : EntityCommandModule<Dissertation>
    {
        public DissertationHandlers(IRepository<Dissertation> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(Dissertation dissertation)
        {
            return ("Вы действительно хотите удалить диссертацию? " +
                    "Все данные будут утеряны.");
        }
    }

    public class DissertationNavigation : NavigationCommandModule
    {
        public DissertationNavigation(MainViewModel viewModel)
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

            commands.AddCommand<Person>(GlobalCommands.BrowseAdd<Dissertation>(), OnBrowseDissertationAdd);
            commands.AddCommand<Dissertation>(GlobalCommands.BrowseDetails<Dissertation>(), OnBrowseDissertationDetails);
        }

        private void OnBrowseDissertationAdd(Person author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Dissertation { Author = author };
            ViewModel.WorkWindow = new DissertationView.Add(model);
        }

        private void OnBrowseDissertationDetails(Dissertation dissertation)
        {
            if (dissertation == null)
            {
                throw new ArgumentNullException("dissertation");
            }

            ViewModel.WorkWindow = new DissertationView.Root(dissertation);
        }
    }
}