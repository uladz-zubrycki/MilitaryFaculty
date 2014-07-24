using System;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom.CommandHandlers
{
    public class InventiveApplicationHandlers: EntityCommandModule<InventiveApplication>
    {
        public InventiveApplicationHandlers(IRepository<InventiveApplication> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(InventiveApplication book)
        {
            return ("Вы действительно хотите удалить заявку? " +
                    "Все данные будут утеряны.");
        }
    }

    public class InventiveApplicationNavigation : NavigationCommandModule
    {
        public InventiveApplicationNavigation(MainViewModel viewModel)
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

            commands.AddCommand<Professor>(GlobalCommands.BrowseAdd<InventiveApplication>(), 
                                           OnBrowseInventiveApplicationAdd);

            commands.AddCommand<InventiveApplication>(GlobalCommands.BrowseDetails<InventiveApplication>(), 
                                                      OnBrowseInventiveApplicationDetails);
        }

        private void OnBrowseInventiveApplicationAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new InventiveApplication { Author = author };
            ViewModel.WorkWindow = new InventiveApplicationView.Add(model);
        }

        private void OnBrowseInventiveApplicationDetails(InventiveApplication inventiveApplication)
        {
            if (inventiveApplication == null)
            {
                throw new ArgumentNullException("inventiveApplication");
            }

            ViewModel.WorkWindow = new InventiveApplicationView.Root(inventiveApplication);
        }
    }
}
