using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.Attributes;

namespace MilitaryFaculty.Presentation.Core.ViewModels.Entity
{
    public abstract class EntityViewModel<T> : ViewModel<T>
        where T : class
    {
        private ICollection<PropertyViewModel> _properties;
        private object _tag;

        public ICollection<PropertyViewModel> Properties
        {
            get
            {
                if (_properties == null)
                {
                    InitPropeties();
                }

                return _properties;
            }
        }

        public override object Tag
        {
            get { return _tag; }
            set
            {
                Properties.ForEach(p => p.Tag = value);
                SetValue(() => _tag, value);
            }
        }

        protected EntityViewModel(T model)
            : base(model)
        {
            // Empty
        }

        private void InitPropeties()
        {
            var type = GetType();
            _properties = type.GetProperties()
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
    }
}