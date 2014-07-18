using System.Windows;
using System.Windows.Controls;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.Custom
{
    public class EditableViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DisplayViewTemplate { get; set; }
        public DataTemplate EditViewTemplate { get; set; }

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
            if (item is PropertyViewModel)
            {
                var viewModel = (ViewModel) item;
                var viewMode = (EditableViewMode) viewModel.Tag;

                if (viewMode == EditableViewMode.Display)
                {
                    return DisplayViewTemplate;
                }

                if (viewMode == EditableViewMode.Edit)
                {
                    return EditViewTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}