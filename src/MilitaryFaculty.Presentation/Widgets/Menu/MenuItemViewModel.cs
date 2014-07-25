using System;
using System.Windows.Input;
using MilitaryFaculty.Presentation.Annotations;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Widgets.Menu
{
    public class MenuItemViewModel: ViewModel
    {
        public MenuItemViewModel(string text,
                                 ICommand command,
                                 object commandParameter)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentException("text");
            }

            Command = command;
            CommandParameter = commandParameter;
            Text = text;
        }

        public string Text { get; private set; }

        public ICommand Command { get; private set; }

        public object CommandParameter { get; private set; }

        public bool IsEnabled
        {
            get { return Command.CanExecute(CommandParameter); }
        }
    }
}
