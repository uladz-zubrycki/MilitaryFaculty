using System;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class PropertyViewModel: ViewModel
    {
        #region Class Fields

        private readonly Action<object> setter;
        private readonly Func<object> getter;
        
        #endregion // Class Fields

        #region Class Properties

        public string Label { get; set; }

        public object Property
        {
            get { return getter(); }
            set { setter(value); }
        }

        #endregion // Class Properties

        #region Class Constructors

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

            this.getter = getter;
            this.setter = setter;
            this.Label = label;
        }

        #endregion // Class Constructors
    }
}
