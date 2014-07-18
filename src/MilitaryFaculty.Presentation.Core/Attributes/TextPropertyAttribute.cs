using System;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;

namespace MilitaryFaculty.Presentation.Core.Attributes
{
    public class TextPropertyAttribute : PropertyAttribute
    {
        public override PropertyViewModel Create(Func<object> getter, Action<object> setter, string label)
        {
            return new TextPropertyViewModel(getter, setter, label);
        }
    }
}