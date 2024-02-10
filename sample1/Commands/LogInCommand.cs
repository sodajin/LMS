using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Commands
{
    public class LogInCommand : CommandBase
    {
        private LogInViewModel _viewModel;
        private NavigationStore _navigationStore;
        private Func<ViewModelBase> _createAdminDashboardViewModel;


        public LogInCommand(
            LogInViewModel viewModel,
            NavigationStore navigationStore, 
            Func<ViewModelBase> createAdminDashboardViewModel
        ) {
            _viewModel = viewModel;
            _navigationStore = navigationStore;
            _createAdminDashboardViewModel = createAdminDashboardViewModel;
        }

        public override void Execute(object parameter)
        {
            string username = "Test";
            string password = "password";

            if (_viewModel.MemberUsername == username && _viewModel.MemberPassword == password)
            {
                _viewModel.AuthMessage = "WELCOME";
                _navigationStore.CurrentViewModel = _createAdminDashboardViewModel();
                return;
            }

            _viewModel.AuthMessage = "INVALID CREDENTIALS";
            _viewModel.ShakeAuth = true;
            _viewModel.ShakeAuth = false;
        }
    }
}
