using System;
using System.Collections.Generic;
using MilitaryFaculty.Presentation.Widgets.TreeView.Events;

namespace MilitaryFaculty.Presentation.Widgets.TreeView
{
    public interface ITreeViewModel
    {
        IEnumerable<ITreeItemViewModel> Items { get; }
        ITreeItemViewModel Selected { get; }

        event EventHandler<SelectedChangedEventArgs> SelectedItemChanged;

        IEnumerable<ITreeItemViewModel> Find(Func<ITreeItemViewModel, bool> predicate);
    }
}