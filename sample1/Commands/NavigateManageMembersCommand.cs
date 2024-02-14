using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Commands
{
    public class NavigateManageMembersCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly List<User> _results;
        private readonly string _searchText;
        private readonly Func<List<User>, string, ViewModelBase> _createViewModel;


        public NavigateManageMembersCommand(NavigationStore navigationStore, List<User> results, string searchText, Func<List<User>, string, ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _results = results;
            _searchText = searchText;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel(_results, _searchText);
        }
    }
}
