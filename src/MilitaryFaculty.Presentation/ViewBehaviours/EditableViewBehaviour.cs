using System;
using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Common;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.Command;
using Omu.ValueInjecter;

namespace MilitaryFaculty.Presentation.ViewBehaviours
{
    public enum EditableViewMode
    {
        Display,
        Edit
    }

    public class EditableViewBehaviour<TModel> : IViewBehaviour<TModel>
        where TModel : class, new()
    {
        private readonly ImagedCommandViewModel _editCommandViewModel;
        private readonly ImagedCommandViewModel _saveCommandViewModel;
        private readonly ImagedCommandViewModel _cancelCommandViewModel;
        private readonly List<CommandViewModel> _commandsBackup;
        private readonly TModel _modelBackup;
        
        private ViewModel<TModel> _viewModel;

        public EditableViewBehaviour(ICommand saveCommand,
                                     string saveTooltip,
                                     string saveImageSource,
                                     string editTooltip,
                                     string editImageSource,
                                     string cancelTooltip,
                                     string cancelImageSource)
        {
            _saveCommandViewModel = CreateSaveCommand(saveCommand, saveTooltip, saveImageSource);
            _editCommandViewModel = CreateEditCommand(editTooltip, editImageSource);
            _cancelCommandViewModel = CreateCancelCommand(cancelTooltip, cancelImageSource);

            _commandsBackup = new List<CommandViewModel>();
            _modelBackup = new TModel();
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

        private ImagedCommandViewModel CreateCancelCommand(string cancelTooltip,
                                                           string cancelImageSource)
        {
            if (String.IsNullOrEmpty(cancelTooltip))
            {
                throw new ArgumentNullException("cancelTooltip");
            }

            if (String.IsNullOrEmpty(cancelImageSource))
            {
                throw new ArgumentNullException("cancelImageSource");
            }

            Action cancel =
                () =>
                {
                    RestoreModel();
                    ToDisplayMode();
                };

            var command = new SimpleCommand(cancel);

            return new ImagedCommandViewModel(command,
                                              cancelTooltip,
                                              cancelImageSource);
        }

        private ImagedCommandViewModel CreateSaveCommand(ICommand command,
                                                         string tooltip,
                                                         string imageSource)
        {
            if (command == null)
            {
                throw new ArgumentNullException("saveCommand");
            }

            if (String.IsNullOrEmpty(tooltip))
            {
                throw new ArgumentNullException("saveTooltip");
            }

            if (String.IsNullOrEmpty(imageSource))
            {
                throw new ArgumentNullException("saveImageSource");
            }

            Action save =
                () =>
                {
                    command.Execute(_viewModel.Model);
                    ToDisplayMode();
                };

            Func<bool> canSave =
                () => command.CanExecute(_viewModel.Model);

            var saveCommand = new SimpleCommand(save, canSave);
            var commandViewModel = new ImagedCommandViewModel(saveCommand,
                                                              tooltip,
                                                              imageSource);
            return commandViewModel;
        }

        private ImagedCommandViewModel CreateEditCommand(string tooltip, string imageSource)
        {
            if (String.IsNullOrEmpty(tooltip))
            {
                throw new ArgumentNullException("saveTooltip");
            }

            if (String.IsNullOrEmpty(imageSource))
            {
                throw new ArgumentNullException("saveImageSource");
            }

            var editCommand = new SimpleCommand(ToEditMode);
            var commandViewModel = new ImagedCommandViewModel(editCommand,
                                                              tooltip,
                                                              imageSource);
            return commandViewModel;
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
            _modelBackup.InjectFrom(_viewModel.Model);
        }

        private void BackupCommands()
        {
            _viewModel.Commands.Remove(_editCommandViewModel);
            _commandsBackup.AddRange(_viewModel.Commands);
        }

        private void RestoreModel()
        {
            _viewModel.Model.InjectFrom(_modelBackup);
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