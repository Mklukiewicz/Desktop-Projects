﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoApp.Core.Helpers
{
    public class RelayCommandParam : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommandParam(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);

        }
    }
}
