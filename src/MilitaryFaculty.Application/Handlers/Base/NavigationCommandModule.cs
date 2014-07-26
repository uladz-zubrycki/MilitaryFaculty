using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public abstract class NavigationCommandModule : ICommandModule
    {
        protected readonly MainViewModel ViewModel;

        protected NavigationCommandModule(MainViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            ViewModel = viewModel;
        }

        public abstract void LoadModule(RoutedCommands commands);
    }
}
