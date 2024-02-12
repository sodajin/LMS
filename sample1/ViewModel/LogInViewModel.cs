using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        private readonly Func<ViewModelBase> _createMemberDashboardViewModel;
        private readonly Func<User, ViewModelBase> _createAdminDashboardViewModel;
        private readonly NavigationStore _navigationStore;
        private readonly UserList _users;
        private string _memberID;
        public string MemberID
        {
            get => _memberID;
            set
            {
                _memberID = value;
                OnPropertyChanged(nameof(_memberID));
            }
        }

        private string _memberUsername;
        public string MemberUsername
        {
            get => _memberUsername;
            set
            {
                _memberUsername = value;
                OnPropertyChanged(nameof(_memberUsername));
            }
        }
        private string _memberPassword;
        public string MemberPassword
        {
            get => _memberPassword;
            set
            {
                _memberPassword = value;
                OnPropertyChanged(nameof(_memberPassword));
            }
        }

        private string _authMessage;
        public string AuthMessage
        {
            get => _authMessage;
            set
            {
                _authMessage = value;
                OnPropertyChanged(nameof(AuthMessage));
            }
        }


        private bool _shakeAuth = false;
        public bool ShakeAuth
        {
            get => _shakeAuth;
            set
            {
                _shakeAuth = value;
                OnPropertyChanged(nameof(ShakeAuth));
            }
        }
        
        private void InvalidCredentials(object parameter)
        {
            AuthMessage = "INVALID CREDENTIALS";
        }

        public ICommand LogInButton { get; }

        public LogInViewModel(
            NavigationStore navigationStore, 
            UserList users,
            Func<ViewModelBase> createMemberDashboardViewModel,
            Func<User, ViewModelBase> createAdminDashboardViewModel) 
        {
            _navigationStore = navigationStore;
            _users = users;
            _createMemberDashboardViewModel = createMemberDashboardViewModel;
            _createAdminDashboardViewModel = createAdminDashboardViewModel;
            LogInButton = new LogInCommand(this, _navigationStore, _createMemberDashboardViewModel, _createAdminDashboardViewModel, _users, this.InvalidCredentials);
        }
    }
}
