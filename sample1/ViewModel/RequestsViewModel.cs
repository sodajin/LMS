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
    public class RequestsViewModel : ViewModelBase
    {
        public Library _library;
        private List<RequestedBook> _books => _library.GetRequestedBooks();
        private readonly ObservableCollection<RequestedBooksTableViewModel> _bookTable;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<ViewModelBase> _createRequestsViewModel;

        private int _selectIndex = -1;

        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                OnPropertyChanged(nameof(_selectIndex));
                AcceptRequestCommand = new AcceptRequestCommand(_library, this, _dashboardNavigationStore, _createRequestsViewModel);
                OnPropertyChanged(nameof(AcceptRequestCommand));
                DenyRequestCommand = new DenyRequestCommand(_library, this, _dashboardNavigationStore, _createRequestsViewModel);
                OnPropertyChanged(nameof(DenyRequestCommand));
            }
        }

        public IEnumerable<RequestedBooksTableViewModel> BookTable => _bookTable;

        public ICommand AcceptRequestCommand { get; set; }
        public ICommand DenyRequestCommand { get; set; }

        public RequestsViewModel(Library library, NavigationStore dashboardNavigationStore, Func<ViewModelBase> createRequestsViewModel)
        {
            _library = library;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createRequestsViewModel = createRequestsViewModel;

            _bookTable = new ObservableCollection<RequestedBooksTableViewModel>();
            IEnumerable<RequestedBook> IBookEnumerable = _books;
            IEnumerator<RequestedBook> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new RequestedBooksTableViewModel(book_enumerate.Current));
            }

            AcceptRequestCommand = new AcceptRequestCommand(_library, this, _dashboardNavigationStore, _createRequestsViewModel);
            DenyRequestCommand = new DenyRequestCommand(_library, this, _dashboardNavigationStore, _createRequestsViewModel);
        }

    }
}
