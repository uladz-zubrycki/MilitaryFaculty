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
                throw new ArgumentNullException("browseDetailsCommand");
            }

            const string editTooltip = "Редактировать";
            const string editImageSource = @"\Content\edit.png";

            const string saveTooltip = "Ок";
            const string saveImageSource = @"\Content\ok.png";

            const string cancelTooltip = "Отмена";
            const string cancelImageSource = @"\Content\cancel.png";

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

        public static void Removable<T>(this ViewModel<T> @this,
                                        ICommand removeCommand)
            where T : class
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (removeCommand == null)
            {
                throw new ArgumentNullException("browseDetailsCommand");
            }

            const string tooltip = "Удалить";
            const string imageSource = @"\Content\remove.png";

            var behaviour = new HasCommandViewBehaviour<T>(removeCommand,
                                                           tooltip,
                                                           imageSource);

            behaviour.Inject(@this);
        }

        public static void Browsable<T>(this ViewModel<T> @this,
                                        ICommand browseDetailsCommand)
            where T : class
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (browseDetailsCommand == null)
            {
                throw new ArgumentNullException("browseDetailsCommand");
            }

            const string tooltip = "Подробно";
            const string imageSource = @"\Content\details.png";

            var behaviour = new HasCommandViewBehaviour<T>(browseDetailsCommand,
                                                           tooltip,
                                                           imageSource);

            behaviour.Inject(@this);
        }
    }
}