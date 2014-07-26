using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Common;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.Handlers
{
    public class NavigationHistory : NavigationCommandModule
    {
        private const int HistorySize = 10;

        private readonly LinkedList<ViewModel> _backHistory;
        private readonly LinkedList<ViewModel> _forwardHistory;

        public NavigationHistory(MainViewModel viewModel)
            : base(viewModel)
        {
            _backHistory = new LinkedList<ViewModel>();
            _forwardHistory = new LinkedList<ViewModel>();

            ViewModel.WorkWindowChanged += ViewModelOnWorkWindowChanged;
        }

        public override void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("sink");
            }

            commands.AddCommand(GlobalCommands.BrowseBack,
                                OnBrowseBack,
                                CanBrowseBack);

            commands.AddCommand(GlobalCommands.BrowseForward,
                                OnBrowseForward,
                                CanBrowseForward);
        }

        private void ViewModelOnWorkWindowChanged(object sender, WorkWindowChangedEventArgs e)
        {
            if (IsChangedByBrowseBack(e) || IsChangedByBrowseForward(e))
            {
                return;
            }

            _forwardHistory.Clear();
            AddToHistory(_backHistory, e.OldValue);
        }

        private void OnBrowseBack()
        {
            var previous = _backHistory.First.Value;
            AddToHistory(_forwardHistory, ViewModel.WorkWindow);
            ViewModel.WorkWindow = previous;

            _backHistory.RemoveFirst();
        }

        private bool CanBrowseBack()
        {
            return _backHistory != null && _backHistory.Count > 0;
        }

        private void OnBrowseForward()
        {
            var next = _forwardHistory.First.Value;
            AddToHistory(_backHistory, ViewModel.WorkWindow);
            ViewModel.WorkWindow = next;

            _forwardHistory.RemoveFirst();
        }

        private bool CanBrowseForward()
        {
            return _forwardHistory != null && _forwardHistory.Count > 0;
        }

        private void AddToHistory(LinkedList<ViewModel> history, ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (history.Count >= HistorySize)
            {
                _forwardHistory.RemoveLast();
            }

            if (history.IsEmpty())
            {
                history.AddFirst(viewModel);
            }
            else if (history.First.Value != viewModel)
            {
                history.AddFirst(viewModel);
            }
        }

        private bool IsChangedByBrowseForward(WorkWindowChangedEventArgs e)
        {
            return _forwardHistory.First != null &&
                   e.NewValue == _forwardHistory.First.Value;
        }

        private bool IsChangedByBrowseBack(WorkWindowChangedEventArgs e)
        {
            return _backHistory.First != null &&
                   e.NewValue == _backHistory.First.Value;
        }
    }
}