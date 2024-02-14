using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Commands
{
    public class SearchUserCommand : CommandBase
    {
        private readonly UserList _userList;
        private readonly string _searchText;
        private readonly Func<List<User>, string, ViewModelBase> _createSearchMemberViewModel;
        private readonly NavigationStore _dashboardNavigationStore;

        public SearchUserCommand(UserList userList, string searchText, NavigationStore dashboardNavigationStore, Func<List<User>, string, ViewModelBase> createSearchMemberViewModel) 
        {
            Trace.WriteLine(searchText);
            _userList = userList;
            _searchText = searchText;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createSearchMemberViewModel = createSearchMemberViewModel;
        }
        public override void Execute(object parameter)
        {
            List<User> results = _userList.SearchUserByString(_searchText);
            _dashboardNavigationStore.CurrentViewModel = _createSearchMemberViewModel(results, _searchText);
        }
    }
}
