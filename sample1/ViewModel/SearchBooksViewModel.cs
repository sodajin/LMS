using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class SearchBooksViewModel : ViewModelBase
    {
        public Library _library;
        private List<Book> _books;
        private readonly ObservableCollection<BookTableViewModel> _bookTable;
        private readonly Func<List<Book>, string, ViewModelBase> _createSearchBookViewModel;
        private readonly Func<Book, List<Book>, string, ViewModelBase> _createBookViewModel;
        private readonly NavigationStore _dashboardNavigationStore;

        public IEnumerable<BookTableViewModel> BookTable => _bookTable;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(_searchText));
                SearchBooksCommand = new SearchBooksCommand(_library, _searchText, _dashboardNavigationStore, _createSearchBookViewModel);
                OnPropertyChanged(nameof(SearchBooksCommand));
            }
        }

        private int _selectIndex = -1;
        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
                _dashboardNavigationStore.CurrentViewModel = _createBookViewModel(
                    _library.GetBookFromID(BookTable.ElementAt(_selectIndex).ID),
                    _books,
                    _searchText
                );
            }
        }


        public ICommand SearchBooksCommand { get; set; }

        public SearchBooksViewModel(Library library, List<Book> results, string searchText, NavigationStore dashboardNavigationStore, Func<List<Book>, string, ViewModelBase> createSearchBookViewModel, Func<Book, List<Book>, string, ViewModelBase> createBookViewModel)
        {
            _library = library;
            _searchText = searchText;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createSearchBookViewModel = createSearchBookViewModel;
            _createBookViewModel = createBookViewModel;

            if (results.Count == 0 && searchText == "") 
            {
                _books = _library.GetBooks();
            }
            else 
            {
                _books = results;
            }

            _bookTable = new ObservableCollection<BookTableViewModel>();
            IEnumerable<Book> IBookEnumerable = _books;
            IEnumerator<Book> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new BookTableViewModel(book_enumerate.Current));
            }

            SearchBooksCommand = new SearchBooksCommand(library, searchText, _dashboardNavigationStore, createSearchBookViewModel);
        }
    }
}
