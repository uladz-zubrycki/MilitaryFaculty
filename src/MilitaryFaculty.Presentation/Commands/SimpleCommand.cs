using System;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Commands
{
    /// <summary>
    ///     Non parametrized realization of SimpleCommand pattern.
    /// </summary>
    public class SimpleCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _command;

        /// <summary>
        ///     Creates new instance of <see cref="SimpleCommand" />
        /// </summary>
        /// <param name="command">Method to be called when the command is invoked.</param>
        public SimpleCommand(Action command)
            : this(command, null)
        {
            //Empty
        }

        /// <summary>
        ///     Creates new instance of <see cref="SimpleCommand" />
        /// </summary>
        /// <param name="command">Method to be called when the command is invoked.</param>
        /// <param name="canExecute">Method that determines whether the command can execute in its current state.</param>
        public SimpleCommand(Action command, Func<bool> canExecute)
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
            return CanExecute();
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
            Execute();
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute()
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        public void Execute()
        {
            _command();
        }
    }
}