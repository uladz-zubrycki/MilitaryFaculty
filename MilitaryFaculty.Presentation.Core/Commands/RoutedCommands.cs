using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Core.Commands
{
    /// <summary>
    ///     It provides a means of registering commands and their callback
    ///     methods, and will invoke those callbacks upon request.
    /// </summary>
    /// <remarks>
    ///     Used for registering RoutedCommands inside view models, but not code-behind files.
    /// </remarks>
    public class RoutedCommands
    {
        private readonly Dictionary<RoutedCommand, ICommand> _commands =
            new Dictionary<RoutedCommand, ICommand>();

        public bool CanExecuteCommand(RoutedCommand command, object parameter, out bool handled)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (_commands.ContainsKey(command))
            {
                handled = true;
                return _commands[command].CanExecute(parameter);
            }

            return (handled = false);
        }

        public void ExecuteCommand(RoutedCommand command, object parameter, out bool handled)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (_commands.ContainsKey(command))
            {
                handled = true;
                _commands[command].Execute(parameter);
            }
            else
            {
                handled = false;
            }
        }

        public void AddCommand(RoutedCommand command, Action execute)
        {
            AddCommand(command, execute, null);
        }

        public void AddCommand(RoutedCommand command, Action execute, Func<bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _commands[command] = new Command(execute, canExecute);
        }

        public void AddCommand<T>(RoutedCommand command, Action<T> execute)
        {
            AddCommand(command, execute, null);
        }

        public void AddCommand<T>(RoutedCommand command, Action<T> execute, Func<T, bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _commands[command] = new Command<T>(execute, canExecute);
        }

        public void RemoveCommand(RoutedCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (_commands.ContainsKey(command))
            {
                _commands.Remove(command);
            }
        }
    }
}