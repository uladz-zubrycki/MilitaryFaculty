using System;

namespace MilitaryFaculty.Presentation.Core.ViewModels.Entity
{
    public class IntPropertyViewModel : PropertyViewModel
    {
        public IntPropertyViewModel(Func<object> getter, Action<object> setter, string label)
            : base(getter, setter, label)
        {
            // Empty
        }
    }
}