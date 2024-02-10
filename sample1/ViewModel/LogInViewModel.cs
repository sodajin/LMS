using LibraryManagementSystem.Commands;
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
        private readonly Func<ViewModelBase> _createAdminDashboardViewModel;
        private readonly NavigationStore _navigationStore;
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
                OnPropertyChanged(nameof(_authMessage));
            }
        }


        private bool _shakeAuth;
        public bool ShakeAuth
        {
            get { return _shakeAuth; }
            set
            {
                if (_shakeAuth != value)
                {
                    _shakeAuth = value;
                    OnPropertyChanged(nameof(ShakeAuth));
                }
            }
        }

        public ICommand LogInButton { get; }

        public LogInViewModel(NavigationStore navigationStore, Func<ViewModelBase> createAdminDashboardViewModel) 
        {
            _navigationStore = navigationStore;
            _createAdminDashboardViewModel = createAdminDashboardViewModel;
            LogInButton = new LogInCommand(this, _navigationStore, _createAdminDashboardViewModel);
        }
    }
}
