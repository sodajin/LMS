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
        private List<BorrowedBook> _books => _library.GetBorrowedBooks();
        private readonly ObservableCollection<BorrowedBooksTableViewModel> _bookTable;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<ViewModelBase> _createManageBookViewModel;

        public IEnumerable<BorrowedBooksTableViewModel> BookTable => _bookTable;

        private int _selectIndex = -1;

        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
            }
        }

        public ICommand ReturnCommand { get; }

        public BorrowedBooksViewModel(Library library, NavigationStore dashboardNavigationStore, Func<ViewModelBase> createManageBookViewModel)
        {
            _library = library;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createManageBookViewModel = createManageBookViewModel;
            _bookTable = new ObservableCollection<BorrowedBooksTableViewModel>();
            IEnumerable<BorrowedBook> IBookEnumerable = _books;
            IEnumerator<BorrowedBook> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new BorrowedBooksTableViewModel(book_enumerate.Current));
            }

            ReturnCommand = new NavigateCommand(_dashboardNavigationStore, _createManageBookViewModel);
        }


    }
}
