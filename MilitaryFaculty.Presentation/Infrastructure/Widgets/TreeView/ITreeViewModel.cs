using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public interface ITreeViewModel
    {
        IEnumerable<ITreeItemViewModel> Items { get; }
        ITreeItemViewModel Selected { get; }

        event EventHandler<SelectedChangedEventArgs> SelectedItemChanged;

        IEnumerable<ITreeItemViewModel> Find(Func<ITreeItemViewModel, bool> predicate);
    }
}