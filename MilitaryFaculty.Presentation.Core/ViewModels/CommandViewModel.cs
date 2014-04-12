using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Core.ViewModels
{
    public abstract class CommandViewModel : ViewModel<ICommand>
    {
        public string Tooltip { get; private set; }
        public object Parameter { get; set; }

        protected CommandViewModel(ICommand model, string tooltip)
            : base(model)
        {
            Tooltip = tooltip;
        }

        protected CommandViewModel(ICommand model, object parameter, string tooltip)
            : base(model)
        {
            Parameter = parameter;
            Tooltip = tooltip;
        }
    }
}