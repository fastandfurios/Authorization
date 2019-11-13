using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Authorization.ViewModel
{
    public class CommutatorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public CommutatorCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter) => _execute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
}
