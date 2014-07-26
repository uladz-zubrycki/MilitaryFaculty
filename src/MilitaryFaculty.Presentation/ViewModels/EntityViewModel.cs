using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Common;
using MilitaryFaculty.Presentation.Annotations;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels.Properties;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public abstract class EntityViewModel<T> : ViewModel<T>
        where T : class
    {
        private readonly Lazy<IEnumerable<PropertyViewModel>> _properties;
        private readonly IList<PropertyViewModel> _registeredProperties;

        [UsedImplicitly] private object _tag;

        protected EntityViewModel(T model)
            : base(model)
        {
            _registeredProperties = new List<PropertyViewModel>();
            _properties = Lazy.Create(InitProperties);
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

        //TODO change property registration system, no attributes and reflection, register them with methods
        protected void HasProperty(PropertyViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            _registeredProperties.Add(viewModel);
        }

        private IEnumerable<PropertyViewModel> InitProperties()
        {
            var reflectedProperties = ReflectProperties();
            var allProperties = reflectedProperties.Union(_registeredProperties);

            return allProperties;
        }

        private IEnumerable<PropertyViewModel> ReflectProperties()
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