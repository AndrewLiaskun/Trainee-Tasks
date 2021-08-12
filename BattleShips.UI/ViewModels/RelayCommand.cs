// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Windows.Input;

namespace BattleShips.UI.ViewModels
{
    // TODO: rework command class
    // 1) Add generic version i.e. RelayCommand<TObject> where TObject is some parameter type
    // 2) Create hierarchy of classes: BaseCommand, GenericCommand<T> and RelayCommand (without generic)
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action executeAction, Func<bool> canExecute = null)
        {
            if (executeAction is null)
                throw new ArgumentNullException(nameof(executeAction));

            _execute = (_) => executeAction();
            _canExecute = canExecute is null ? ((_) => true) : new Func<object, bool>((_) => canExecute());
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? ((_) => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}