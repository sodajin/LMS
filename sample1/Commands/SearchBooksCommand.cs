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
    public class SearchBooksCommand : CommandBase
    {
        private readonly Library _books;
        private readonly string _searchText;
        private readonly Func<List<Book>, string, ViewModelBase> _createSearchBookViewModel;
        private readonly NavigationStore _dashboardNavigationStore;

        public SearchBooksCommand(Library books, string searchText, NavigationStore dashboardNavigationStore, Func<List<Book>, string, ViewModelBase> createSearchBookViewModel) 
        {
            Trace.WriteLine(searchText);
            _books = books;
            _searchText = searchText;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createSearchBookViewModel = createSearchBookViewModel;
        }
        public override void Execute(object parameter)
        {
            List<Book> results = _books.SearchBook(_searchText);
            _dashboardNavigationStore.CurrentViewModel = _createSearchBookViewModel(results, _searchText);
        }
    }
}
