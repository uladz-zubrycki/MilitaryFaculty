using System;
using System.Collections.Generic;
using MilitaryFaculty.Presentation.Core.Commands;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class CommonNavigationModule : BaseNavigationModule
    {
        private const int HistorySize = 10;

        private readonly LinkedList<ViewModel> _backHistory;
        private readonly LinkedList<ViewModel> _forwardHistory;

        public CommonNavigationModule(MainViewModel viewModel)
            : base(viewModel)
        {
            _backHistory = new LinkedList<ViewModel>();
            _forwardHistory = new LinkedList<ViewModel>();

            ViewModel.WorkWindowChanged += ViewModelOnWorkWindowChanged;
        }

        public override void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("sink");
            }

            container.RegisterCommand(Browse.Back,
                OnBrowseBack,
                CanBrowseBack);

            container.RegisterCommand(Browse.Forward,
                OnBrowseForward,
                CanBrowseForward);
        }

        private void ViewModelOnWorkWindowChanged(object sender, WorkWindowChangedEventArgs e)
        {
            if (IsChangedByBrowseBack(e) || IsChangedByBrowseForward(e))
            {
                return;
            }

            AddToBackHistory(e.OldValue);
        }

        private void OnBrowseBack()
        {
            var previous = _backHistory.First.Value;
            AddToForwardHistory(ViewModel.WorkWindow);
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
            AddToBackHistory(ViewModel.WorkWindow);
            ViewModel.WorkWindow = next;

            _forwardHistory.RemoveFirst();
        }

        private bool CanBrowseForward()
        {
            return _forwardHistory != null && _forwardHistory.Count > 0;
        }

        private void AddToBackHistory(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (_backHistory.Count >= HistorySize)
            {
                _backHistory.RemoveLast();
            }

            _backHistory.AddFirst(viewModel);
        }

        private void AddToForwardHistory(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (_forwardHistory.Count >= HistorySize)
            {
                _forwardHistory.RemoveLast();
            }

            _forwardHistory.AddFirst(viewModel);
        }

        private bool IsChangedByBrowseForward(WorkWindowChangedEventArgs e)
        {
            return _forwardHistory.First != null && e.NewValue == _forwardHistory.First.Value;
        }

        private bool IsChangedByBrowseBack(WorkWindowChangedEventArgs e)
        {
            return _backHistory.First != null && e.NewValue == _backHistory.First.Value;
        }
    }
}