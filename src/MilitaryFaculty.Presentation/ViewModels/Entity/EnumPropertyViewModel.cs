using System;

namespace MilitaryFaculty.Presentation.ViewModels.Entity
{
    public class EnumPropertyViewModel : PropertyViewModel
    {
        public EnumPropertyViewModel(Func<object> getter, Action<object> setter, string label)
            : base(getter, setter, label)
        {
            // Empty
        }
    }
}