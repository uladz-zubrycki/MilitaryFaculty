using System;
using System.Windows.Input;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Widgets.Command
{
    public abstract class CommandViewModel : ViewModel<ICommand>
    {
        public string Tooltip { get; private set; }
        public object Parameter { get; set; }

        protected CommandViewModel(ICommand model, string tooltip)
            : base(model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (String.IsNullOrEmpty(tooltip))
            {
                throw new ArgumentException();
            }

            Tooltip = tooltip;
        }

        protected CommandViewModel(ICommand model, object parameter, string tooltip)
            : this(model, tooltip)
        {
            Parameter = parameter;
        }
    }
}