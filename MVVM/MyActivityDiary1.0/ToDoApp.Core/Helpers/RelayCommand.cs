using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoApp.Core.Helpers
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action mAction;

        public RelayCommand(Action action)// W DEPENDENCY INJECTION PARAMETR ZAWSZE 2 
        {
            mAction = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            mAction();
        }
    }
}
