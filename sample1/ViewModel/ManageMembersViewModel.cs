using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class ManageMembersViewModel : ViewModelBase
    {
        private UserList _userList;
        private List<User> _users => _userList.GetSimpleUsers();
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly ObservableCollection<MemberTableViewModel> _memberTable;
        private readonly Func<ViewModelBase> _createAddMemberViewModel;
        private readonly Func<ViewModelBase> _createManageMembersViewModel;

        public IEnumerable<MemberTableViewModel> MemberTable => _memberTable;

        public ICommand AddMemberCommand { get; }
        public ICommand PenalizeCommand { get; set; }


        private int _selectIndex = -1;

        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
                PenalizeCommand = new PenalizeCommand(this, _userList, _selectIndex, _dashboardNavigationStore, _createManageMembersViewModel);
                OnPropertyChanged(nameof(PenalizeCommand));
            }
        }

        public ManageMembersViewModel(UserList userList, NavigationStore dashboardNavigationStore, Func<ViewModelBase> createAddMemberViewModel, Func<ViewModelBase> createManageMembersViewModel)
        {
            _userList = userList;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createAddMemberViewModel = createAddMemberViewModel;
            _createManageMembersViewModel = createManageMembersViewModel;

            _memberTable = new ObservableCollection<MemberTableViewModel>();
            IEnumerable<User> IMemberEnumerable = _users;
            IEnumerator<User> member_enumerate = IMemberEnumerable.GetEnumerator();

            while (member_enumerate.MoveNext())
            {
                _memberTable.Add(new MemberTableViewModel(member_enumerate.Current));
            }

            AddMemberCommand = new NavigateCommand(_dashboardNavigationStore, _createAddMemberViewModel);
            PenalizeCommand = new PenalizeCommand(this, _userList, _selectIndex, _dashboardNavigationStore, _createManageMembersViewModel);
        }
    }
}
