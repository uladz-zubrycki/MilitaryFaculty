using System;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom
{
    public abstract class BaseNavigationModule : ICommandModule
    {
        protected readonly MainViewModel ViewModel;

        protected BaseNavigationModule(MainViewModel viewModel)
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