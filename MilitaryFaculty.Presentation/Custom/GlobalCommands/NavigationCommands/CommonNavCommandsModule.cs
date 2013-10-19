using System;
using System.Collections.Generic;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class CommonNavCommandsModule : BaseNavCommandsModule
    {
        #region Class Constants

        private const int HistorySize = 10;

        #endregion // Class Constants

        #region Class Fields

        private readonly LinkedList<ViewModel> backHistory;
        private readonly LinkedList<ViewModel> forwardHistory;

        #endregion // Class Fields

        #region Class Constructors

        public CommonNavCommandsModule(MainViewModel viewModel)
            : base(viewModel)
        {
            backHistory = new LinkedList<ViewModel>();
            forwardHistory = new LinkedList<ViewModel>();

            ViewModel.WorkWindowChanged += ViewModelOnWorkWindowChanged;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public override void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("sink");
            }

            container.RegisterCommand(GlobalNavCommands.BrowseBack,
                                      OnBrowseBack,
                                      CanBrowseBack);
            container.RegisterCommand(GlobalNavCommands.BrowseForward,
                                      OnBrowseForward,
                                      CanBrowseForward);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void ViewModelOnWorkWindowChanged(object sender, WorkWindowChangedEventArgs e)
        {
            // WorkWindow changed by BrowseBackCommand 
            if (backHistory.First != null && e.NewValue == backHistory.First.Value)
            {
                return;
            }

            //WorkWIndow cahnged by BrowseForwardCommand
            if (forwardHistory.First != null && e.NewValue == forwardHistory.First.Value)
            {
                return;
            }

            AddToBackHistory(e.OldValue);
        }

        private void OnBrowseBack()
        {
            var previous = backHistory.First.Value;
            AddToForwardHistory(ViewModel.WorkWindow);
            ViewModel.WorkWindow = previous;

            backHistory.RemoveFirst();
        }

        private bool CanBrowseBack()
        {
            return backHistory != null && backHistory.Count > 0;
        }

        private void OnBrowseForward()
        {
            var next = forwardHistory.First.Value;
            AddToBackHistory(ViewModel.WorkWindow);
            ViewModel.WorkWindow = next;

            forwardHistory.RemoveFirst();
        }

        private bool CanBrowseForward()
        {
            return forwardHistory != null && forwardHistory.Count > 0;
        }

        private void AddToBackHistory(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (backHistory.Count >= HistorySize)
            {
                backHistory.RemoveLast();
            }

            backHistory.AddFirst(viewModel);
        }

        private void AddToForwardHistory(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (forwardHistory.Count >= HistorySize)
            {
                forwardHistory.RemoveLast();
            }

            forwardHistory.AddFirst(viewModel);
        }

        #endregion // Class Private Methods
    }
}