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
    public class SearchBorrowedBooksCommand : CommandBase
    {
        private readonly Library _books;
        private readonly string _searchText;
        private readonly Func<List<BorrowedBook>, string, ViewModelBase> _createSearchBorrowedBookViewModel;
        private readonly NavigationStore _dashboardNavigationStore;

        public SearchBorrowedBooksCommand(Library books, string searchText, NavigationStore dashboardNavigationStore, Func<List<BorrowedBook>, string, ViewModelBase> createSearchBorrowedBookViewModel) 
        {
            Trace.WriteLine(searchText);
            _books = books;
            _searchText = searchText;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createSearchBorrowedBookViewModel = createSearchBorrowedBookViewModel;
        }
        public override void Execute(object parameter)
        {
            List<BorrowedBook> results = _books.SearchBorrowedBookByString(_searchText);
            _dashboardNavigationStore.CurrentViewModel = _createSearchBorrowedBookViewModel(results, _searchText);
        }
    }
}
