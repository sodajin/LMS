using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using System.Windows;

namespace LibraryManagementSystem.ViewModel
{
    public class AdminDashboardViewModel : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;
        private readonly Library _library;
        private readonly UserList _userList;
        private readonly User _user;

        private NavigationStore _dashboardNavigationStore;
        public ViewModelBase CurrentDashboardViewModel => _dashboardNavigationStore.CurrentViewModel;

        public ICommand SearchBooksCommand { get; }
        public ICommand RequestsCommand { get; }
        public ICommand ManageBooksCommand { get; }
        public ICommand ManageMembersCommand { get; }
        public ICommand LogOutCommand { get; }

        public string Name => _user.FirstName;

        public AdminDashboardViewModel(User user, NavigationStore navigationStore, Func<ViewModelBase> createLogInViewModel, Library library, UserList userList)
        {
            _user = user;
            _navigationStore = navigationStore;
            _library = library;
            _userList = userList;
            _dashboardNavigationStore = new NavigationStore();
            _dashboardNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            SearchBooksCommand = new NavigateSearchBooksCommand(_dashboardNavigationStore, new List<Book>(), "", CreateSearchBooksViewModel);
            RequestsCommand = new NavigateCommand(_dashboardNavigationStore, CreateRequestsViewModel);
            ManageBooksCommand = new NavigateManageBooksCommand(_dashboardNavigationStore, new List<Book>(), "", CreateManageBooksViewModel);
            ManageMembersCommand = new NavigateManageMembersCommand(_dashboardNavigationStore, new List<User>(), "", CreateManageMembersViewModel);
            LogOutCommand = new LogOutCommand(_navigationStore, createLogInViewModel);

            if (_library.GetRequestedBooks().Count > 0)
            {
                MessageBox.Show($"You have {_library.GetRequestedBooks().Count} request(s).", "Requests", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private SearchBooksViewModel CreateSearchBooksViewModel(List<Book> results, string searchText)
        {
            return new SearchBooksViewModel(_library, results, searchText, _dashboardNavigationStore, CreateSearchBooksViewModel);
        }
        private RequestsViewModel CreateRequestsViewModel()
        {
            return new RequestsViewModel(_library, _dashboardNavigationStore, CreateRequestsViewModel);
        }
        private ManageBooksViewModel CreateManageBooksViewModel(List<Book> results, string searchText)
        {
            return new ManageBooksViewModel(_library, results, searchText, _dashboardNavigationStore, CreateAddBookViewModel, CreateEditBookViewModel, CreateBorrowedBooksViewModel, CreateManageBooksViewModel);
        }
        private ManageMembersViewModel CreateManageMembersViewModel(List<User> results, string searchText)
        {
            return new ManageMembersViewModel(_userList, results, searchText, _dashboardNavigationStore, CreateAddMemberViewModel, CreateManageMembersViewModel);
        }
        private AddBookViewModel CreateAddBookViewModel()
        {
            return new AddBookViewModel(_library, _dashboardNavigationStore, CreateManageBooksViewModel);
        }
        private EditBookViewModel CreateEditBookViewModel(ulong index)
        {
            return new EditBookViewModel(_library, _dashboardNavigationStore, CreateManageBooksViewModel, index);
        }
        private BorrowedBooksViewModel CreateBorrowedBooksViewModel(List<BorrowedBook> results, string searchText)
        {
            return new BorrowedBooksViewModel(_library, results, searchText, _dashboardNavigationStore, CreateManageBooksViewModel, CreateBorrowedBooksViewModel);
        }

        private AddMemberViewModel CreateAddMemberViewModel()
        {
            return new AddMemberViewModel(_userList, _dashboardNavigationStore, CreateManageMembersViewModel);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentDashboardViewModel));
        }
    }
}
