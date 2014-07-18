using System;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Presentation.Attributes
{
    public class TextPropertyAttribute : PropertyAttribute
    {
        public override PropertyViewModel Create(Func<object> getter, Action<object> setter, string label)
        {
            return new TextPropertyViewModel(getter, setter, label);
        }
    }
}