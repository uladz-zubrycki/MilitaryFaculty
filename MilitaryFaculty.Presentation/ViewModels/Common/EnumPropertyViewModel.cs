using System;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class EnumPropertyViewModel : PropertyViewModel
    {
        public EnumPropertyViewModel(Func<object> getter, Action<object> setter, string label)
            : base(getter, setter, label)
        {
            // Empty
        }
    }
}