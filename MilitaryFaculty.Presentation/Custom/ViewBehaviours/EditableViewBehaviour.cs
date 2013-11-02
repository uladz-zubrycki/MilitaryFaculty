using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class EditableViewBehaviour<T> : IViewBehaviour
        where T : class, IImitator<T>, new()
    {
        #region Class Fields

        private ViewModel<T> viewModel;

        private readonly List<CommandViewModel> commandsBackup;
        private readonly T modelBackup;

        private readonly ImagedCommandViewModel editCommandViewModel;
        private readonly ImagedCommandViewModel applyChangesCommandViewModel;
        private readonly ImagedCommandViewModel cancelChangesCommandViewModel;

        #endregion // Class Fields

        #region Class Constructors

        public EditableViewBehaviour(ICommand applyCommand, object commandParameter)
        {
            if (applyCommand == null)
            {
                throw new ArgumentNullException("applyCommand");
            }

            commandsBackup = new List<CommandViewModel>();
            modelBackup = new T();

            applyChangesCommandViewModel = CreateApplyChangesCommand(applyCommand, commandParameter);
            editCommandViewModel = CreateEditCommand();
            cancelChangesCommandViewModel = CreateCancelCommand();
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void Inject(ViewModel<T> viewModel)
        {
            Inject(viewModel, EditableViewMode.Display);
        }

        public void Inject(ViewModel<T> viewModel, EditableViewMode mode)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (!mode.IsDefined())
            {
                throw new InvalidEnumArgumentException();
            }

            this.viewModel = viewModel;

            if (mode == EditableViewMode.Display)
            {
                ToDisplayMode();
            }
            else 
            {
                ToEditMode();
            }
        }

        #endregion // Class Public Methods

        #region Implementation of IViewBehaviour

        void IViewBehaviour.Inject(ViewModel viewModel)
        {
            Inject(viewModel as ViewModel<T>);
        }

        void IViewBehaviour.Inject(ViewModel viewModel, object defaultTag)
        {
            Inject(viewModel as ViewModel<T>, (EditableViewMode)defaultTag);
        }

        #endregion // Implementation of IViewBehaviour

        #region Class Private Methods

        private ImagedCommandViewModel CreateCancelCommand()
        {
            const string tooltip = "Отмена";
            const string imagePath = @"..\Content\cancel.png";
            var command = new Command(OnCancelChanges);

            return new ImagedCommandViewModel(command, tooltip, imagePath);
        }

        private ImagedCommandViewModel CreateApplyChangesCommand(ICommand applyCommand, object parameter)
        {
            const string tooltip = "Ок";
            const string imagePath = @"..\Content\ok.png";

            Action onApply = () =>
                             {
                                 applyCommand.Execute(parameter);
                                 ToDisplayMode();
                             };

            Func<bool> canApply = () => applyCommand.CanExecute(parameter);
 
            var command = new Command(onApply, canApply);

            return new ImagedCommandViewModel(command, tooltip, imagePath);
        }

        private ImagedCommandViewModel CreateEditCommand()
        {
            const string tooltip = "Редактировать";
            const string imagePath = @"..\Content\edit.png";
            var command = new Command(ToEditMode);

            return new ImagedCommandViewModel(command, tooltip, imagePath);
        }

        private void ToDisplayMode()
        {
            RestoreCommands();

            viewModel.Tag = EditableViewMode.Display;
        }

        private void ToEditMode()
        {
            BackupCommands();
            BackupModel();

            viewModel.Commands.Clear();
            viewModel.Commands.Add(applyChangesCommandViewModel);
            viewModel.Commands.Add(cancelChangesCommandViewModel);

            viewModel.Tag = EditableViewMode.Edit;
        }

        private void BackupModel()
        {
            modelBackup.Imitate(viewModel.Model);
        }

        private void BackupCommands()
        {
            viewModel.Commands.Remove(editCommandViewModel);
            commandsBackup.AddRange(viewModel.Commands);
        }

        private void RestoreModel()
        {
            viewModel.Model.Imitate(modelBackup);
        }

        private void RestoreCommands()
        {
            viewModel.Commands.Clear();
            viewModel.Commands.AddRange(commandsBackup);
            viewModel.Commands.Add(editCommandViewModel);

            commandsBackup.Clear();
        }

        private void OnCancelChanges()
        {
            RestoreModel();
            ToDisplayMode();
        }

        #endregion // Class Private Methods
    }
}