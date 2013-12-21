using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class EntityViewModel<T>: ViewModel<T> 
        where T : class
    {
        #region Class Fields

        private ICollection<PropertyViewModel> properties;
        private object tag;

        #endregion // Class Fields

        #region Class Properties

        public ICollection<PropertyViewModel> Properties
        {
            get
            {
                if (properties == null)
                {
                    InitPropeties();
                }

                return properties;
            }
        }

        #region Overrides of ViewModel

        public override object Tag
        {
            get { return tag; }
            set
            {
                Properties.ForEach(p => p.Tag = value);
                SetValue(() => tag, value);
            }
        }

        #endregion

        #endregion // Class Properties

        #region Class Constructors

        protected EntityViewModel(T model)
            : base(model)
        {
            // Empty
        }  

        #endregion // Class Constructors

        #region Class Private Methods

        private void InitPropeties()
        {
            var type = GetType();
            properties = type.GetProperties()
                        .Where(p => p.HasAttribute<PropertyAttribute>())
                        .Select(CreatePropertyViewModel)
                        .ToList();
        }

        private PropertyViewModel CreatePropertyViewModel(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<PropertyAttribute>();
            var getter = CreatePropertyGetter(property);
            var setter = CreatePropertySetter(property);
            var label = attribute.Label ?? property.Name;

            return attribute.Create(getter, setter, label);
        }

        private Func<object> CreatePropertyGetter(PropertyInfo property)
        {
            return () => property.GetValue(this);
        }

        private Action<object> CreatePropertySetter(PropertyInfo property)
        {
            return x => property.SetValue(this, x);
        }

        #endregion // Class Private Methods
    }
}
