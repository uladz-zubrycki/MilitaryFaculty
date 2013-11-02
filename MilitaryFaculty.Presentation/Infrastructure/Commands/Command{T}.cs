﻿using System;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    /// <summary>
    /// Strong-typed parametrized realization of Command pattern.
    /// </summary>
    public class Command<T> : ICommand
    {
        #region Class Fields

        private readonly Action<T> command;
        private readonly Func<T, bool> canExecute;

        #endregion // Class Fields 

        #region Class Events

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion // Class Events

        #region Class Constructors

        /// <summary>
        /// Creates new instance of <see cref="Command{T}"/>
        /// </summary>
        /// <param name="command">Method to be called when the command is invoked.</param>
        public Command(Action<T> command)
            : this(command, null)
        {
            //Empty
        }

        /// <summary>
        /// Creates new instance of <see cref="Command{T}"/>
        /// </summary>
        /// <param name="command">Method to be called when the command is invoked.</param>
        /// <param name="canExecute">Method that determines whether the command can execute in its current state.</param>
        public Command(Action<T> command, Func<T, bool> canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            this.command = command;
            this.canExecute = canExecute;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="param">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        public bool CanExecute(T param)
        {
            return canExecute == null || canExecute(param);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="param">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        public void Execute(T param)
        {
            command(param);
        }

        #endregion // Class Public Methods

        #region Implementation of ICommand 

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="param">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
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
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="param">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        void ICommand.Execute(object param)
        {
            if (param != null && !(param is T))
            {
                throw new ArgumentException();
            }

            Execute((T) param);
        }

        #endregion // Implementation of ICommand
    }
}