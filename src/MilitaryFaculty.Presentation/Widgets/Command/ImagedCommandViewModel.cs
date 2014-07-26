using System;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Widgets.Command
{
    public class ImagedCommandViewModel : CommandViewModel
    {
        public string ImageSource { get; private set; }

        public ImagedCommandViewModel(ICommand model,
                                      string tooltip,
                                      string imageSrc)
            : base(model, tooltip)
        {
            if (imageSrc == null)
            {
                throw new ArgumentNullException("imageSrc");
            }

            ImageSource = imageSrc;
        }

        public ImagedCommandViewModel(ICommand model,
                                      object parameter,
                                      string tooltip,
                                      string imageSrc)
            : base(model, parameter, tooltip)
        {
            if (imageSrc == null)
            {
                throw new ArgumentNullException("imageSrc");
            }

            ImageSource = imageSrc;
        }
    }
}