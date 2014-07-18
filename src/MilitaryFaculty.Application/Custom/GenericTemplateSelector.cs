using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.Custom
{
    internal class GenericTemplateSelector : DataTemplateSelector
    {
        //TODO: too bad, string literals so deep
        private static readonly IDictionary<Type, string> KnownResources =
            new Dictionary<Type, string>
            {
                {typeof (EntityViewModel<>), "EntityViewTemplate"},
                {typeof (EntityRootViewModel<>), "EntityRootViewTemplate"},
                {typeof (AddEntityViewModel<>), "AddEntityViewTemplate"},
                {typeof (ListItemViewModel<>), "ListItemViewTemplate"},
            };

        /// <summary>
        ///     When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.
        /// </summary>
        /// <returns>
        ///     Returns a <see cref="T:System.Windows.DataTemplate" /> or null. The default value is null.
        /// </returns>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                var type = item.GetType();

                var resourceKey = KnownResources.FirstOrDefault(type.IsClosedTypeOf);

                if (resourceKey != null)
                {
                    return FindResource(resourceKey);
                }
            }

            return base.SelectTemplate(item, container);
        }

        private static DataTemplate FindResource(string resourceKey)
        {
            return (DataTemplate) System.Windows.Application.Current.FindResource(resourceKey);
        }
    }
}