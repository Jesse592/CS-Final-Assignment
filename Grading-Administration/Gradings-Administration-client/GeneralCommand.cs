﻿using System;
using System.Windows.Input;

namespace GradingAdmin_client.ViewModels
{
    class GeneralCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        readonly Action<object> ExecuteCommand;
        readonly Predicate<object> CanExecuteAction;

        /// <summary>
        /// Constructor for General command, canExecute is set to null
        /// </summary>
        /// <param name="execute">The action performed when method is called</param>
        public GeneralCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Main constructor for GeneralCommand
        /// </summary>
        /// <param name="execute">The action performed when method is called</param>
        /// <param name="canExecute">The function that determens if the action is allowed</param>
        public GeneralCommand(Action<object> execute, Predicate<object> canExecute)
        {

            ExecuteCommand = execute;
            CanExecuteAction = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecuteAction == null ? true : CanExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteCommand(parameter);
        }
    }
}