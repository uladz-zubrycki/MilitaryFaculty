using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class EditableViewBehaviour<TModel> : IViewBehaviour
        where TModel : class, IImitator<TModel>, new()
    {
        #region Class Fields

        private ViewModel<TModel> viewModel;

        private readonly List<CommandViewModel> commandsBackup;
        private readonly TModel modelBackup;

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
            modelBackup = new TModel();

            applyChangesCommandViewModel = CreateApplyChangesCommand(applyCommand, commandParameter);
            editCommandViewModel = CreateEditCommand();
            cancelChangesCommandViewModel = CreateCancelCommand();
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void Inject(ViewModel<TModel> viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            this.viewModel = viewModel;
            this.viewModel.Tag = EditableViewMode.Display;
            this.viewModel.Commands.Add(editCommandViewModel);
        }

        #endregion // Class Public Methods

        #region Implementation of IViewBehaviour

        void IViewBehaviour.Inject(ViewModel viewModel)
        {
            Inject(viewModel as ViewModel<TModel>);
        }

        #endregion // Implementation of IViewBehaviour

        #region Class Private Methods

        private ImagedCommandViewModel CreateCancelCommand()
        {
            const string tooltip = "Отмена";
            const string imagePath = @"..\Content\cancel.png";

            Action onCancel = () =>
            {
                RestoreModel();
                ToDisplayMode();
            };

            var command = new Command(onCancel);

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
            viewModel.Commands.AddRange(new[]
                                        {
                                            applyChangesCommandViewModel,
                                            cancelChangesCommandViewModel
                                        });

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

        #endregion // Class Private Methods
    }
}