using System;
using System.Windows.Input;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.Custom
{
    public static class ViewModelExtensions
    {
        public static void Editable<T>(this ViewModel<T> @this, ICommand saveCommand)
            where T : class, new()
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (saveCommand == null)
            {            
                throw new ArgumentNullException("addCommand");
            }

            const string editTooltip = "Редактировать";
            const string editImageSource = @"\Content\images\edit.png";

            const string saveTooltip = "Ок";
            const string saveImageSource = @"\Content\images\ok.png";

            const string cancelTooltip = "Отмена";
            const string cancelImageSource = @"\Content\images\cancel.png";

            var behaviour =
                new EditableViewBehaviour<T>(saveCommand,
                                             saveTooltip,
                                             saveImageSource,
                                             editTooltip,
                                             editImageSource,
                                             cancelTooltip,
                                             cancelImageSource);

            behaviour.Inject(@this);
        }

        public static void Removable<T>(this ViewModel<T> @this, ICommand removeCommand)
            where T : class
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (removeCommand == null)
            {
                throw new ArgumentNullException("addCommand");
            }

            const string tooltip = "Удалить";
            const string imageSource = @"\Content\images\remove.png";

            var behaviour = new HasCommandViewBehaviour<T>(removeCommand,
                                                           tooltip,
                                                           imageSource);

            behaviour.Inject(@this);
        }

        public static void Browsable<T>(this ViewModel<T> @this, ICommand browseDetailsCommand)
            where T : class
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (browseDetailsCommand == null)
            {
                throw new ArgumentNullException("addCommand");
            }

            const string tooltip = "Подробно";
            const string imageSource = @"\Content\images\details.png";

            var behaviour = new HasCommandViewBehaviour<T>(browseDetailsCommand,
                                                           tooltip,
                                                           imageSource);

            behaviour.Inject(@this);
        }

        public static void Addable<T>(this ViewModel<T> @this, ICommand addCommand)
            where T : class
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (addCommand == null)
            {
                throw new ArgumentNullException("addCommand");
            }

            const string tooltip = "Добавить";
            const string imageSource = @"\Content\images\add.png";

            var behaviour = new HasCommandViewBehaviour<T>(addCommand,
                                                           tooltip,
                                                           imageSource);

            behaviour.Inject(@this);
        }
    }
}