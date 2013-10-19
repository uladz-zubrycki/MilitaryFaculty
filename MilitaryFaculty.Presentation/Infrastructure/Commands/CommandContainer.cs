using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    /// <summary>
    /// This implementation of ICommandSink can serve as a base
    /// class for a ViewModel or as an object embedded in a ViewModel.  
    /// It provides a means of registering commands and their callback 
    /// methods, and will invoke those callbacks upon request.
    /// </summary>
    public class CommandContainer : ICommandContainer
    {
        #region Class Fields

        private readonly Dictionary<ICommand, ICommand> commandToCallbackMap =
            new Dictionary<ICommand, ICommand>();

        #endregion // Class Fields

        #region Class Public Methods

        public void RegisterCommand(ICommand command, Action execute)
        {
            RegisterCommand(command, execute, null);
        }

        public void RegisterCommand(ICommand command, Action execute, Func<bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            commandToCallbackMap[command] = new Command(execute, canExecute);
        }

        public void RegisterCommand<T>(ICommand command, Action<T> execute)
        {
            RegisterCommand(command, execute, null);
        }

        public void RegisterCommand<T>(ICommand command, Action<T> execute, Func<T, bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            commandToCallbackMap[command] = new Command<T>(execute, canExecute);
        }

        public void UnregisterCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (commandToCallbackMap.ContainsKey(command))
            {
                commandToCallbackMap.Remove(command);
            }
        }

        #endregion // Class Public Methods

        #region Implementation of ICommandSink 

        public virtual bool CanExecuteCommand(ICommand command, object parameter, out bool handled)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (commandToCallbackMap.ContainsKey(command))
            {
                handled = true;

                return commandToCallbackMap[command].CanExecute(parameter);
            }

            return (handled = false);
        }

        public virtual void ExecuteCommand(ICommand command, object parameter, out bool handled)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (commandToCallbackMap.ContainsKey(command))
            {
                handled = true;
                commandToCallbackMap[command].Execute(parameter);
            }
            else
            {
                handled = false;
            }
        }

        #endregion // Implementation of ICommandSink
    }
}