using System;
using MilitaryFaculty.Presentation.Core.Commands;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public abstract class BaseNavigationModule : ICommandContainerModule
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

        public abstract void RegisterModule(CommandContainer container);
    }
}