using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public class CommandViewModel : ViewModel<ICommand>
    {
        #region Class Properties

        public string Tooltip { get; private set; }
        public object Parameter { get; set; }

        #endregion // Class Properties

        #region Class Constructors

        public CommandViewModel(ICommand model, string tooltip)
            : base(model)
        {
            Tooltip = tooltip;
        }

        public CommandViewModel(ICommand model, object parameter, string tooltip)
            : base(model)
        {
            Parameter = parameter;
            Tooltip = tooltip;
        }

        #endregion // Class Constructors
    }
}