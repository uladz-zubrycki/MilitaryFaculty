using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Attributes;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public abstract class EntityViewModel<T> : ViewModel<T>
        where T : class
    {
        private readonly Lazy<IEnumerable<PropertyViewModel>> _properties;
        private object _tag;

        protected EntityViewModel(T model)
            : base(model)
        {
            _properties = Lazy.Create(CreateProperties);
        }

        public IEnumerable<PropertyViewModel> Properties
        {
            get { return _properties.Value; }
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

        private IEnumerable<PropertyViewModel> CreateProperties()
        {
            var type = GetType();
            var result = type.GetProperties()
                             .Where(p => p.HasAttribute<PropertyAttribute>())
                             .Select(CreatePropertyViewModel)
                             .ToList();

            return result;
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