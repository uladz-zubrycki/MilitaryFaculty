using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class CommandViewModel : ViewModel<ICommand>
    {
        #region Class Properties

        public string Tooltip { get; private set; }
        public object Parameter { get; set; }

        #endregion // Class Properties

        #region Class Constructors

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

        #endregion // Class Constructors
    }
}