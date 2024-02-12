using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.Commands
{
    public class LogInCommand : CommandBase
    {
        private readonly LogInViewModel _viewModel;
        private readonly UserList _users;
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createMemberDashboardViewModel;
        private readonly Func<ViewModelBase> _createAdminDashboardViewModel;
        private readonly Action<object> _changeAuthMessage;
        private readonly Predicate<object> _canExecute;

        public LogInCommand(
            LogInViewModel viewModel,
            NavigationStore navigationStore,
            Func<ViewModelBase> createMemberDashboardViewModel,
            Func<ViewModelBase> createAdminDashboardViewModel,
            UserList users,
            Action<object> changeAuthMessage,
            Predicate<object> canExecute = null
        ) {
            _viewModel = viewModel;
            _navigationStore = navigationStore;
            _createMemberDashboardViewModel = createMemberDashboardViewModel;
            _createAdminDashboardViewModel = createAdminDashboardViewModel;
            _users = users;
            _changeAuthMessage = changeAuthMessage ?? throw new ArgumentNullException(nameof(changeAuthMessage));

        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public override void Execute(object parameter)
        {

            User credentials = _users.SignIn(_viewModel.MemberID, _viewModel.MemberUsername, _viewModel.MemberPassword);

            if (credentials == null)
            {
                _changeAuthMessage(parameter);
                _viewModel.ShakeAuth = true;
                _viewModel.ShakeAuth = false;
                return;
            }

            if (credentials.AccountType == AccountType.Admin) 
            {
                _navigationStore.CurrentViewModel = _createAdminDashboardViewModel();
                return;
            }

            if (credentials.AccountType == AccountType.Simple)
            {
                _navigationStore.CurrentViewModel = _createMemberDashboardViewModel();
                return;
            }
        }
    }
}
