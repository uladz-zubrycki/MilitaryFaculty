using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    /// <summary>
    ///     It provides a means of registering commands and their callback
    ///     methods, and will invoke those callbacks upon request.
    /// </summary>
    /// <remarks>
    ///     Used for registering RoutedCommands inside view models, but not code-behind files.
    /// </remarks>
    public class CommandContainer : ICommandContainer
    {
        private readonly Dictionary<RoutedCommand, ICommand> _commandToCallbackMap =
            new Dictionary<RoutedCommand, ICommand>();

        public virtual bool CanExecuteCommand(RoutedCommand command, object parameter, out bool handled)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (_commandToCallbackMap.ContainsKey(command))
            {
                handled = true;

                return _commandToCallbackMap[command].CanExecute(parameter);
            }

            return (handled = false);
        }

        public virtual void ExecuteCommand(RoutedCommand command, object parameter, out bool handled)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (_commandToCallbackMap.ContainsKey(command))
            {
                handled = true;
                _commandToCallbackMap[command].Execute(parameter);
            }
            else
            {
                handled = false;
            }
        }

        public void RegisterCommand(RoutedCommand command, Action execute)
        {
            RegisterCommand(command, execute, null);
        }

        public void RegisterCommand(RoutedCommand command, Action execute, Func<bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _commandToCallbackMap[command] = new Command(execute, canExecute);
        }

        public void RegisterCommand<T>(RoutedCommand command, Action<T> execute)
        {
            RegisterCommand(command, execute, null);
        }

        public void RegisterCommand<T>(RoutedCommand command, Action<T> execute, Func<T, bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _commandToCallbackMap[command] = new Command<T>(execute, canExecute);
        }

        public void UnregisterCommand(RoutedCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (_commandToCallbackMap.ContainsKey(command))
            {
                _commandToCallbackMap.Remove(command);
            }
        }
    }
}