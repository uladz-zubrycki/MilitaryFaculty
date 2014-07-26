using System;

namespace MilitaryFaculty.Presentation.ViewModels.Properties
{
    public class TextPropertyViewModel : PropertyViewModel
    {
        public TextPropertyViewModel(Func<object> getter, Action<object> setter, string name)
            : base(getter, setter, name)
        {
            // Empty
        }
    }
}