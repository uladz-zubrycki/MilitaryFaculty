using System;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.ViewModels.Properties;

namespace MilitaryFaculty.Presentation.Attributes
{
    public class IntPropertyAttribute : PropertyAttribute
    {
        public override PropertyViewModel Create(Func<object> getter, Action<object> setter, string label)
        {
            return new IntPropertyViewModel(getter, setter, label);
        }
    }
}