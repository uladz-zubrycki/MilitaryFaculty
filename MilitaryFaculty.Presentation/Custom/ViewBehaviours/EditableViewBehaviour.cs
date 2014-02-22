using System;
using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class EditableViewBehaviour<TModel> : IViewBehaviour
        where TModel : class, IImitator<TModel>, new()
    {
        private readonly ImagedCommandViewModel _applyChangesCommandViewModel;
        private readonly ImagedCommandViewModel _cancelChangesCommandViewModel;
        private readonly List<CommandViewModel> _commandsBackup;
        private readonly ImagedCommandViewModel _editCommandViewModel;
        private readonly TModel _modelBackup;
        private ViewModel<TModel> _viewModel;

        public EditableViewBehaviour(ICommand applyCommand, object commandParameter)
        {
            if (applyCommand == null)
            {
                throw new ArgumentNullException("applyCommand");
            }

            _commandsBackup = new List<CommandViewModel>();
            _modelBackup = new TModel();

            _applyChangesCommandViewModel = CreateApplyChangesCommand(applyCommand, commandParameter);
            _editCommandViewModel = CreateEditCommand();
            _cancelChangesCommandViewModel = CreateCancelCommand();
        }

        void IViewBehaviour.Inject(ViewModel viewModel)
        {
            Inject(viewModel as ViewModel<TModel>);
        }

        public void Inject(ViewModel<TModel> viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            _viewModel = viewModel;
            _viewModel.Tag = EditableViewMode.Display;
            _viewModel.Commands.Add(_editCommandViewModel);
        }

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

            _viewModel.Tag = EditableViewMode.Display;
        }

        private void ToEditMode()
        {
            BackupCommands();
            BackupModel();

            _viewModel.Commands.Clear();
            _viewModel.Commands.AddRange(new[]
                                         {
                                             _applyChangesCommandViewModel,
                                             _cancelChangesCommandViewModel
                                         });

            _viewModel.Tag = EditableViewMode.Edit;
        }

        private void BackupModel()
        {
            _modelBackup.Imitate(_viewModel.Model);
        }

        private void BackupCommands()
        {
            _viewModel.Commands.Remove(_editCommandViewModel);
            _commandsBackup.AddRange(_viewModel.Commands);
        }

        private void RestoreModel()
        {
            _viewModel.Model.Imitate(_modelBackup);
        }

        private void RestoreCommands()
        {
            _viewModel.Commands.Clear();
            _viewModel.Commands.AddRange(_commandsBackup);
            _viewModel.Commands.Add(_editCommandViewModel);

            _commandsBackup.Clear();
        }
    }
}