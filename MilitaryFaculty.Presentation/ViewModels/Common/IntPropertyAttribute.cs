using System;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class IntPropertyAttribute : PropertyAttribute
    {
        public override PropertyViewModel Create(Func<object> getter, Action<object> setter, string label)
        {
            return new IntPropertyViewModel(getter, setter, label);
        }
    }
}