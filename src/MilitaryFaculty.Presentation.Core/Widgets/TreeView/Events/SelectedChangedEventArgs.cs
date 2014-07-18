using System;

namespace MilitaryFaculty.Presentation.Core.Widgets.TreeView.Events
{
    public class SelectedChangedEventArgs : EventArgs
    {
        public ITreeItemViewModel NewValue { get; private set; }
        public ITreeItemViewModel OldValue { get; private set; }

        public SelectedChangedEventArgs(ITreeItemViewModel newValue, ITreeItemViewModel oldValue)
        {
            if (newValue == null)
            {
                throw new ArgumentNullException("newValue");
            }

            NewValue = newValue;
            OldValue = oldValue;
        }
    }
}