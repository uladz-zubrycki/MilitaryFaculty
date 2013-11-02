using System;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public abstract class BaseNavigationCommands : ICommandContainerModule
    {
        #region Class Fields

        protected readonly MainViewModel ViewModel;

        #endregion // Class Fields

        #region Class Constructors

        protected BaseNavigationCommands(MainViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            this.ViewModel = viewModel;
        }

        #endregion // Class Constructors

        #region Class Abstract Methods

        public abstract void RegisterModule(CommandContainer container);

        #endregion // Class Abstract Methods
    }
}