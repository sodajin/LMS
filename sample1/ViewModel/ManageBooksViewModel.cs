using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class ManageBooksViewModel : ViewModelBase
    {
        public Library _library;
        private List<Book> _books;
        private readonly ObservableCollection<BookTableViewModel> _bookTable;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<int, ViewModelBase> _createEditBookViewModel;
        private readonly Func<List<Book>, string, ViewModelBase> _createManageBookViewModel;

        public IEnumerable<BookTableViewModel> BookTable => _bookTable;

        private int _selectIndex = -1;

        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
                EditBookCommand = new SelectEditBookCommand(this, _library, _bookTable[SelectIndex].ID, _dashboardNavigationStore, _createEditBookViewModel);
                OnPropertyChanged(nameof(EditBookCommand)); 
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
                SearchBooksCommand = new SearchBooksCommand(_library, _searchText, _dashboardNavigationStore, _createManageBookViewModel);
                OnPropertyChanged(nameof(SearchBooksCommand));
            }
        }

        private Genre _genre = Genre.None;
        public Genre Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(_genre));
                FilterBooks(_genre);
            }
        }

        public ObservableCollection<Genre> GenreItems => new ObservableCollection<Genre>(Enum.GetValues(typeof(Genre)).Cast<Genre>());

        public void FilterBooks(Genre genre)
        {
            if (_genre != Genre.None)
            {
                _books = _library.GetBooksByGenre(_genre);
            }
            else
            {
                _books = _library.GetBooks();
            }
            _bookTable.Clear();
            IEnumerable<Book> IBookEnumerable = _books;
            IEnumerator<Book> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new BookTableViewModel(book_enumerate.Current));
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand EditBookCommand { get; set;  }
        public ICommand ViewBorrowedBooksCommand { get; }
        public ICommand SearchBooksCommand { get; set; }

        public ManageBooksViewModel(
            Library library, List<Book> results, string searchText,
            NavigationStore dashboardNavigationStore, 
            Func<ViewModelBase> createAddBookViewModel, 
            Func<int, ViewModelBase> createEditBookViewModel,
            Func<List<BorrowedBook>, string, ViewModelBase> createBorrowedBooksViewModel,
            Func<List<Book>, string, ViewModelBase> createManageBookViewModel
        ) {
            _library = library;
            _searchText = searchText;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createEditBookViewModel = createEditBookViewModel;
            _createManageBookViewModel = createManageBookViewModel;

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

            AddBookCommand = new NavigateCommand(_dashboardNavigationStore, createAddBookViewModel);
            EditBookCommand = new SelectEditBookCommand(this, _library, 0, _dashboardNavigationStore, createEditBookViewModel);
            ViewBorrowedBooksCommand = new NavigateBorrowedBooksCommand(_dashboardNavigationStore, new List<BorrowedBook>(), "", createBorrowedBooksViewModel);
            SearchBooksCommand = new SearchBooksCommand(_library, _searchText, _dashboardNavigationStore, _createManageBookViewModel);
        }

    }
}
