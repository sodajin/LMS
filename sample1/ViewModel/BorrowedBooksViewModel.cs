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
    public class BorrowedBooksViewModel : ViewModelBase
    {
        public Library _library;
        private List<BorrowedBook> _books;
        private readonly ObservableCollection<BorrowedBooksTableViewModel> _bookTable;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<Book>, string, ViewModelBase> _createManageBookViewModel;
        private readonly Func<List<BorrowedBook>, string, ViewModelBase> _createBorrowedBooksViewModel;

        public IEnumerable<BorrowedBooksTableViewModel> BookTable => _bookTable;

        private int _selectIndex = -1;

        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
                ReturnBookCommand = new ReturnBookCommand(_library, this, _bookTable[_selectIndex].ID, _dashboardNavigationStore, _createBorrowedBooksViewModel);
                OnPropertyChanged(nameof(ReturnBookCommand));
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
                SearchBorrowedBooksCommand = new SearchBorrowedBooksCommand(_library, _searchText, _dashboardNavigationStore, _createBorrowedBooksViewModel);
                OnPropertyChanged(nameof(SearchBorrowedBooksCommand));
            }
        }

        public ICommand SearchBorrowedBooksCommand { get; set; }
        public ICommand ReturnBookCommand { get; set; }
        public ICommand BackCommand { get; }

        public BorrowedBooksViewModel(
            Library library,
            List<BorrowedBook> results,
            string searchText,
            NavigationStore dashboardNavigationStore,
            Func<List<Book>, string, ViewModelBase> createManageBookViewModel,
            Func<List<BorrowedBook>, string, ViewModelBase> createBorrowedBooksViewModel)
        {
            _library = library;
            _searchText = searchText;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createManageBookViewModel = createManageBookViewModel;
            _createBorrowedBooksViewModel = createBorrowedBooksViewModel;

            if (results.Count == 0 && searchText == "")
            {
                _books = _library.GetBorrowedBooks();
            }
            else
            {
                _books = results;
            }


            _bookTable = new ObservableCollection<BorrowedBooksTableViewModel>();
            IEnumerable<BorrowedBook> IBookEnumerable = _books;
            IEnumerator<BorrowedBook> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new BorrowedBooksTableViewModel(book_enumerate.Current));
            }

            SearchBorrowedBooksCommand = new SearchBorrowedBooksCommand(_library, _searchText, _dashboardNavigationStore, _createBorrowedBooksViewModel);
            ReturnBookCommand = new ReturnBookCommand(_library, this, 0, _dashboardNavigationStore, _createBorrowedBooksViewModel);
            BackCommand = new NavigateManageBooksCommand(_dashboardNavigationStore, new List<Book>(), "", _createManageBookViewModel);
        }


    }
}
