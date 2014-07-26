using System;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Commands
{
    /// <summary>
    ///     Strong-typed parametrized realization of SimpleCommand pattern.
    /// </summary>
    public class SimpleCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _command;

        /// <summary>
        ///     Creates new instance of <see cref="SimpleCommand{T}" />
        /// </summary>
        /// <param name="command">Method to be called when the command is invoked.</param>
        public SimpleCommand(Action<T> command)
            : this(command, null)
        {
            //Empty
        }

        /// <summary>
        ///     Creates new instance of <see cref="SimpleCommand{T}" />
        /// </summary>
        /// <param name="command">Method to be called when the command is invoked.</param>
        /// <param name="canExecute">Method that determines whether the command can execute in its current state.</param>
        public SimpleCommand(Action<T> command, Func<T, bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            _command = command;
            _canExecute = canExecute;
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="param">
        ///     Data used by the command. If the command does not require data to be passed,
        ///     this object can be set to null.
        /// </param>
        bool ICommand.CanExecute(object param)
        {
            if (param != null && !(param is T))
            {
                throw new ArgumentException();
            }

            return CanExecute((T) param);
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="param">
        ///     Data used by the command. If the command does not require data to be passed,
        ///     this object can be set to null.
        /// </param>
        void ICommand.Execute(object param)
        {
            if (param != null && !(param is T))
            {
                throw new ArgumentException();
            }

            Execute((T) param);
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="param">
        ///     Data used by the command. If the command does not require data to be passed,
        ///     this object can be set to null.
        /// </param>
        public bool CanExecute(T param)
        {
            return _canExecute == null || _canExecute(param);
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="param">
        ///     Data used by the command. If the command does not require data to be passed,
        ///     this object can be set to null.
        /// </param>
        public void Execute(T param)
        {
            _command(param);
        }
    }
}