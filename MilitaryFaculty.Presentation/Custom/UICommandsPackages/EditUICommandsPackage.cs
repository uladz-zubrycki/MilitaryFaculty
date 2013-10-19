using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

// ReSharper disable InconsistentNaming

namespace MilitaryFaculty.Presentation.Custom
{
    public class EditUICommandsPackage<T> : IUICommandPackage
        where T : class, IImitator<T>, new()
    {
        #region Class Fields

        private ViewModel<T> viewModel;

        private readonly ICommand applyCommand;
        private readonly object commandParameter;

        private readonly List<CommandViewModel> commandsBackup;
        private readonly T modelBackup;

        private ImagedCommandViewModel editCommandViewModel;
        private ImagedCommandViewModel applyChangesCommandViewModel;
        private ImagedCommandViewModel cancelChangesCommandViewModel;

        #endregion // Class Fields

        #region Class Constructors

        public EditUICommandsPackage(ICommand applyCommand, object commandParameter)
        {
            if (applyCommand == null)
            {
                throw new ArgumentNullException("applyCommand");
            }

            this.applyCommand = applyCommand;
            this.commandParameter = commandParameter;

            commandsBackup = new List<CommandViewModel>();
            modelBackup = new T();

            InitCommands();
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void Inject(ViewModel<T> viewModel)
        {
            Inject(viewModel, EditViewMode.Display);
        }

        public void Inject(ViewModel<T> viewModel, object defaultTag)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            if (!Enum.IsDefined(typeof (EditViewMode), defaultTag))
            {
                throw new InvalidEnumArgumentException();
            }

            this.viewModel = viewModel;

            SetMode((EditViewMode) defaultTag);
        }

        #endregion // Class Public Methods

        #region Implementation of IUICommandPackage

        void IUICommandPackage.Inject(ViewModel viewModel)
        {
            Inject(viewModel as ViewModel<T>);
        }

        void IUICommandPackage.Inject(ViewModel viewModel, object defaultTag)
        {
            Inject(viewModel as ViewModel<T>, defaultTag);
        }

        #endregion // Implementation of IUICommandPackage

        #region Class Private Methods

        private void InitCommands()
        {
            var editCommand = new Command(ToEditMode);
            var applyChangesCommand = new Command(OnApplyChanges, CanApplyChanges);
            var cancelChangesCommand = new Command(OnCancelChanges);

            editCommandViewModel = new ImagedCommandViewModel(editCommand, "Редактировать", @"..\Content\edit.png");
            applyChangesCommandViewModel = new ImagedCommandViewModel(applyChangesCommand, "Ок", @"..\Content\ok.png");
            cancelChangesCommandViewModel = new ImagedCommandViewModel(cancelChangesCommand, "Отмена",
                                                                       @"..\Content\cancel.png");
        }

        private void SetMode(EditViewMode mode)
        {
            if (mode == EditViewMode.Display)
            {
                ToDisplayMode();
            }
            else if (mode == EditViewMode.Edit)
            {
                ToEditMode();
            }
        }

        private void ToDisplayMode()
        {
            // restore commands
            viewModel.Commands.Clear();
            viewModel.Commands.AddRange(commandsBackup);
            viewModel.Commands.Add(editCommandViewModel);

            commandsBackup.Clear();

            viewModel.Tag = EditViewMode.Display;
        }

        private void ToEditMode()
        {
            // backup commands
            viewModel.Commands.Remove(editCommandViewModel);
            commandsBackup.AddRange(viewModel.Commands);

            // backup model
            modelBackup.Imitate(viewModel.Model);

            // set "apply", "cancel" commands
            viewModel.Commands.Clear();
            viewModel.Commands.Add(applyChangesCommandViewModel);
            viewModel.Commands.Add(cancelChangesCommandViewModel);

            viewModel.Tag = EditViewMode.Edit;
        }

        private void OnApplyChanges()
        {
            applyCommand.Execute(commandParameter);
            ToDisplayMode();
        }

        private bool CanApplyChanges()
        {
            return applyCommand.CanExecute(commandParameter);
        }

        private void OnCancelChanges()
        {
            viewModel.Model.Imitate(modelBackup);
            ToDisplayMode();
        }

        #endregion // Class Private Methods
    }
}

// ReSharper restore InconsistentNaming