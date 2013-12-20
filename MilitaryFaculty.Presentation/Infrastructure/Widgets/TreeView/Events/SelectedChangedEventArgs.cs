using System;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public class SelectedChangedEventArgs : EventArgs
    {
        #region Class Properties

        public ITreeItemViewModel NewValue { get; private set; }
        public ITreeItemViewModel OldValue { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public SelectedChangedEventArgs(ITreeItemViewModel newValue, ITreeItemViewModel oldValue)
        {
            if (newValue == null)
            {
                throw new ArgumentNullException("newValue");
            }

            NewValue = newValue;
            OldValue = oldValue;
        }

        #endregion // Class Constructors
    }
}