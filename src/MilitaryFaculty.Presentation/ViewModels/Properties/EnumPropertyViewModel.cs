using System;

namespace MilitaryFaculty.Presentation.ViewModels.Properties
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