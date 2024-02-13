using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class ManageMembersViewModel : ViewModelBase
    {
        private readonly UserList _userList;
        private List<User> _users => _userList.GetSimpleUsers();
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly ObservableCollection<MemberTableViewModel> _memberTable;

        public IEnumerable<MemberTableViewModel> MemberTable => _memberTable;

        public ManageMembersViewModel(UserList userList, NavigationStore dashboardNavigationStore)
        {
            _userList = userList;
            _dashboardNavigationStore = dashboardNavigationStore;

            _memberTable = new ObservableCollection<MemberTableViewModel>();
            IEnumerable<User> IBookEnumerable = _users;
            IEnumerator<User> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _memberTable.Add(new MemberTableViewModel(book_enumerate.Current));
            }
        }
    }
}
