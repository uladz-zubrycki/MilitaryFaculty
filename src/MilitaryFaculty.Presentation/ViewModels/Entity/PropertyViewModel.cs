using System;

namespace MilitaryFaculty.Presentation.ViewModels.Entity
{
    public abstract class PropertyViewModel : ViewModel
    {
        private readonly Func<object> _getter;
        private readonly Action<object> _setter;

        public string Label { get; set; }

        public object Property
        {
            get { return _getter(); }
            set { _setter(value); }
        }

        protected PropertyViewModel(Func<object> getter, Action<object> setter, string label)
        {
            if (getter == null)
            {
                throw new ArgumentNullException("accessor");
            }

            if (setter == null)
            {
                throw new ArgumentNullException("setter");
            }

            if (String.IsNullOrEmpty(label))
            {
                throw new ArgumentException("label");
            }

            _getter = getter;
            _setter = setter;
            Label = label;
        }
    }
}