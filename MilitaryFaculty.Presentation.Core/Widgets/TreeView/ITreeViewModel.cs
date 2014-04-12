using System;
using System.Collections.Generic;
using MilitaryFaculty.Presentation.Core.Widgets.TreeView.Events;

namespace MilitaryFaculty.Presentation.Core.Widgets.TreeView
{
    public interface ITreeViewModel
    {
        IEnumerable<ITreeItemViewModel> Items { get; }
        ITreeItemViewModel Selected { get; }

        event EventHandler<SelectedChangedEventArgs> SelectedItemChanged;

        IEnumerable<ITreeItemViewModel> Find(Func<ITreeItemViewModel, bool> predicate);
    }
}