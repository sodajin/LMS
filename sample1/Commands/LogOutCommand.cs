using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Commands
{
    public class LogOutCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createLogInViewModel;
        public LogOutCommand(NavigationStore navigationStore, Func<ViewModelBase> createLogInViewModel) 
        {
            _navigationStore = navigationStore;
            _createLogInViewModel = createLogInViewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult logOutAnswer = MessageBox.Show("Do you wish to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (logOutAnswer == MessageBoxResult.No) 
            {
                return;
            }

            _navigationStore.CurrentViewModel = _createLogInViewModel();
        }
    }
}
