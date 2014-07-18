using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MilitaryFaculty.Presentation.Widgets.TreeView
{
    public interface ITreeItemViewModel
    {
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
        string Title { get; }
        ITreeItemViewModel Parent { get; }
        ObservableCollection<ITreeItemViewModel> Children { get; }

        object Model { get; }

        IEnumerable<ITreeItemViewModel> Find(Func<ITreeItemViewModel, bool> predicate);
    }
}