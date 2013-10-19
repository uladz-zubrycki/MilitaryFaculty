using System;
using System.Windows.Input;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ImagedCommandViewModel : CommandViewModel
    {
        #region Class Properties

        public string ImageSource { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ImagedCommandViewModel(ICommand model, string tooltip, string imageSrc)
            : base(model, tooltip)
        {
            if (imageSrc == null)
            {
                throw new ArgumentNullException("imageSrc");
            }

            ImageSource = imageSrc;
        }

        public ImagedCommandViewModel(ICommand model, object parameter, string tooltip, string imageSrc)
            : base(model, parameter, tooltip)
        {
            if (imageSrc == null)
            {
                throw new ArgumentNullException("imageSrc");
            }

            ImageSource = imageSrc;
        }

        #endregion // Class Constructors
    }
}