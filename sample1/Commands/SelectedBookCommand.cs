using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.Commands
{
    public class SelectedBookCommand : CommandBase
    {
        private readonly Action<object> _enableEditButton;
        private readonly Predicate<object> _canExecute;

        public SelectedBookCommand(Action<object> enableEditButton, Predicate<object> canExecute)
        {
            _enableEditButton = enableEditButton;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public override void Execute(object parameter)
        {
            _enableEditButton(parameter);
        }
    }
}