using System;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class WorkWindowChangedEventArgs : EventArgs
    {
        public ViewModel OldValue { get; set; }
        public ViewModel NewValue { get; set; }

        public WorkWindowChangedEventArgs(ViewModel oldValue, ViewModel newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}