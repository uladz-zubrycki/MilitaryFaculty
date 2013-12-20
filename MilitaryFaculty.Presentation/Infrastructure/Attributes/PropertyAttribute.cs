using System;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class PropertyAttribute : Attribute
    {
        private string label;

        public  string Label
        {
            get { return label; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("value");
                }

                label = value;
            }
        }

        public abstract PropertyViewModel Create(Func<object> getter, Action<object> setter, string label);
    }
}
