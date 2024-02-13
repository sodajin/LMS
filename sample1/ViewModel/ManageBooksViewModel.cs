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
        private List<Book> _books => _library.GetBooks();
        private readonly ObservableCollection<BookTableViewModel> _bookTable;
        private readonly NavigationStore _dashboardNavigationStore;

        public IEnumerable<BookTableViewModel> BookTable => _bookTable;

        private int _selectIndex = -1;

        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
                EditBookCommand = new SelectEditBookCommand(_selectIndex, _books, _dashboardNavigationStore);
                OnPropertyChanged(nameof(EditBookCommand)); 
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand EditBookCommand { get; set;  }
        public ICommand ViewBorrowedBooksCommand { get; }

        public ManageBooksViewModel(Library library, NavigationStore dashboardNavigationStore, Func<ViewModelBase> createAddBookViewModel)
        {
            _library = library;
            _dashboardNavigationStore = dashboardNavigationStore;
            _bookTable = new ObservableCollection<BookTableViewModel>();
            IEnumerable<Book> IBookEnumerable = _books;
            IEnumerator<Book> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new BookTableViewModel(book_enumerate.Current));
            }

            AddBookCommand = new NavigateCommand(_dashboardNavigationStore, createAddBookViewModel);
            EditBookCommand = new SelectEditBookCommand(SelectIndex, _books, _dashboardNavigationStore);
        }

    }
}
