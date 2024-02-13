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
    public class AddMemberViewModel : ViewModelBase
    {
        public readonly UserList _userList;
		public readonly NavigationStore _dashboardNavigationStore;

		private string _ID;
		public string ID
		{
			get => _ID;
			set
			{
				_ID = value;
				OnPropertyChanged(nameof(_ID));
			}
		}

		private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(_firstName));
            }
        }

		private string _middleName;
		public string MiddleName
		{
			get => _middleName;
			set
			{
				_middleName = value;
				OnPropertyChanged(nameof(_middleName));
			}
		}

		private string _lastName;
		public string LastName
		{
			get => _lastName;
			set
			{
				_lastName = value;
				OnPropertyChanged(nameof(_lastName));
			}
		}

		private string _username;
		public string Username
		{
			get => _username;
			set
			{
				_username = value;
				OnPropertyChanged(nameof(_username));
			}
		}

		private string _password;
		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(_password));
			}
		}

		public ICommand AddMemberCommand { get; }
		public ICommand CancelCommand { get; }

		public AddMemberViewModel(UserList userList, NavigationStore _dashboardNavigationStore, Func<ViewModelBase> createManageMemberViewModel) 
        {
            _userList = userList;
            _dashboardNavigationStore = _dashboardNavigationStore;
			AddMemberCommand = new AddMemberCommand(this, _userList, _dashboardNavigationStore, createManageMemberViewModel);
			CancelCommand = new DiscardCommand(_dashboardNavigationStore, createManageMemberViewModel);
        }
    }
}
