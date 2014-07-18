using System;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;

namespace MilitaryFaculty.Presentation.Core.Attributes
{
    public abstract class PropertyAttribute : Attribute
    {
        private string _label;

        public string Label
        {
            get { return _label; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("value");
                }

                _label = value;
            }
        }

        public abstract PropertyViewModel Create(Func<object> getter, Action<object> setter, string label);
    }
}