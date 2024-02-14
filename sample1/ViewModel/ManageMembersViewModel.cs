using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class ManageMembersViewModel : ViewModelBase
    {
        private UserList _userList;
        private List<User> _users;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly ObservableCollection<MemberTableViewModel> _memberTable;
        private readonly Func<ViewModelBase> _createAddMemberViewModel;
        private readonly Func<List<User>, string, ViewModelBase> _createManageMembersViewModel;

        public IEnumerable<MemberTableViewModel> MemberTable => _memberTable;


        private int _selectIndex = -1;

        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
                PenalizeCommand = new PenalizeCommand(this, _userList, _selectIndex, _dashboardNavigationStore, _createManageMembersViewModel);
                ClearPenaltyCommand = new ClearPenaltyCommand(this, _userList, _selectIndex, _dashboardNavigationStore, _createManageMembersViewModel);
                OnPropertyChanged(nameof(PenalizeCommand));
                OnPropertyChanged(nameof(ClearPenaltyCommand));
            }
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(_searchText));
                SearchUserCommand = new SearchUserCommand(_userList, _searchText, _dashboardNavigationStore, _createManageMembersViewModel);
                OnPropertyChanged(nameof(SearchUserCommand));
            }
        }

        public ICommand AddMemberCommand { get; }
        public ICommand PenalizeCommand { get; set; }
        public ICommand ClearPenaltyCommand { get; set; }
        public ICommand SearchUserCommand { get; set; }

        public ManageMembersViewModel(UserList userList, List<User> results, string searchText, NavigationStore dashboardNavigationStore, Func<ViewModelBase> createAddMemberViewModel, Func<List<User>, string, ViewModelBase> createManageMembersViewModel)
        {
            _userList = userList;
            _searchText = searchText;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createAddMemberViewModel = createAddMemberViewModel;
            _createManageMembersViewModel = createManageMembersViewModel;

            if (results.Count == 0 && searchText == "")
            {
                _users = _userList.GetSimpleUsers();
            }
            else
            {
                _users = results;
            }

            _memberTable = new ObservableCollection<MemberTableViewModel>();
            IEnumerable<User> IMemberEnumerable = _users;
            IEnumerator<User> member_enumerate = IMemberEnumerable.GetEnumerator();

            while (member_enumerate.MoveNext())
            {
                _memberTable.Add(new MemberTableViewModel(member_enumerate.Current));
                Trace.WriteLine(member_enumerate.Current.Reputation);
            }

            AddMemberCommand = new NavigateCommand(_dashboardNavigationStore, _createAddMemberViewModel);
            PenalizeCommand = new PenalizeCommand(this, _userList, _selectIndex, _dashboardNavigationStore, _createManageMembersViewModel);
            ClearPenaltyCommand = new ClearPenaltyCommand(this, _userList, _selectIndex, _dashboardNavigationStore, _createManageMembersViewModel);
            SearchUserCommand = new SearchUserCommand(_userList, _searchText, _dashboardNavigationStore, _createManageMembersViewModel);
        }
    }
}
