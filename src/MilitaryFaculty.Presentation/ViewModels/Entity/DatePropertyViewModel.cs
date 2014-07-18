using System;

namespace MilitaryFaculty.Presentation.ViewModels.Entity
{
    public class DatePropertyViewModel : PropertyViewModel
    {
        public DatePropertyViewModel(Func<object> getter, Action<object> setter, string label)
            : base(getter, setter, label)
        {
            // Empty
        }
    }
}