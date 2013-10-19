using System;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class WorkWindowChangedEventArgs : EventArgs
    {
        #region Class Properties

        public ViewModel OldValue { get; set; }
        public ViewModel NewValue { get; set; }

        #endregion // Class Properties

        #region Class Constructors

        public WorkWindowChangedEventArgs(ViewModel oldValue, ViewModel newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion // Class Constructors
    }
}