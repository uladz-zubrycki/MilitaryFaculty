using System;
using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.ViewBehaviours
{
    public static class ViewModelExtensions
    {
        public static void Editable<T>(this ViewModel<T> @this, ICommand saveCommand)
            where T : class, IImitator<T>, new()
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (saveCommand == null)
            {            
                throw new ArgumentNullException("saveCommand");
            }

            new EditableViewBehaviour<T>(saveCommand).Inject(@this);
        }
    }

    public class EditableViewBehaviour<TModel> : IViewBehaviour
        where TModel : class, IImitator<TModel>, new()
    {
        private readonly ImagedCommandViewModel _editCommandViewModel;
        private readonly ImagedCommandViewModel _saveCommandViewModel;
        private readonly ImagedCommandViewModel _cancelCommandViewModel;
        private readonly List<CommandViewModel> _commandsBackup;
        private readonly TModel _modelBackup;

        private ViewModel<TModel> _viewModel;

        public EditableViewBehaviour(ICommand saveCommand)
        {
            if (saveCommand == null)
            {
                throw new ArgumentNullException("saveCommand");
            }

            _commandsBackup = new List<CommandViewModel>();
            _modelBackup = new TModel();

            _saveCommandViewModel = CreateSaveCommand(saveCommand);
            _editCommandViewModel = CreateEditCommand();
            _cancelCommandViewModel = CreateCancelCommand();
        }

        void IViewBehaviour.Inject(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            Inject((ViewModel<TModel>) viewModel);
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

            Action cancel =
                () =>
                {
                    RestoreModel();
                    ToDisplayMode();
                };

            var command = new Command(cancel);

            return new ImagedCommandViewModel(command,
                                              tooltip,
                                              imagePath);
        }

        private ImagedCommandViewModel CreateSaveCommand(ICommand applyCommand)
        {
            const string tooltip = "Ок";
            const string imagePath = @"..\Content\ok.png";

            Action save =
                () =>
                {
                    applyCommand.Execute(_viewModel.Model);
                    ToDisplayMode();
                };

            Func<bool> canSave =
                () => applyCommand.CanExecute(_viewModel.Model);

            var command = new Command(save, canSave);

            return new ImagedCommandViewModel(command,
                                              tooltip,
                                              imagePath);
        }

        private ImagedCommandViewModel CreateEditCommand()
        {
            const string tooltip = "Редактировать";
            const string imagePath = @"..\Content\edit.png";

            var command = new Command(ToEditMode);

            return new ImagedCommandViewModel(command,
                                              tooltip,
                                              imagePath);
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
                                             _saveCommandViewModel,
                                             _cancelCommandViewModel
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