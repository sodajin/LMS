using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;

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
            ManageBooksCommand = new NavigateCommand(_dashboardNavigationStore, CreateManageBooksViewModel);
            ManageMembersCommand = new NavigateCommand(_dashboardNavigationStore, CreateManageMembersViewModel);
            LogOutCommand = new LogOutCommand(_navigationStore, createLogInViewModel);
        }

        private SearchBooksViewModel CreateSearchBooksViewModel(List<Book> results, string searchText)
        {
            return new SearchBooksViewModel(_library, results, searchText, _dashboardNavigationStore, CreateSearchBooksViewModel);
        }
        private RequestsViewModel CreateRequestsViewModel()
        {
            return new RequestsViewModel();
        }
        private ManageBooksViewModel CreateManageBooksViewModel()
        {
            return new ManageBooksViewModel(_library, _dashboardNavigationStore, CreateAddBookViewModel, CreateEditBookViewModel, CreateBorrowedBooksViewModel);
        }
        private ManageMembersViewModel CreateManageMembersViewModel()
        {
            return new ManageMembersViewModel(_userList, _dashboardNavigationStore, CreateAddMemberViewModel, CreateManageMembersViewModel);
        }
        private AddBookViewModel CreateAddBookViewModel()
        {
            return new AddBookViewModel(_library, _dashboardNavigationStore, CreateManageBooksViewModel);
        }
        private EditBookViewModel CreateEditBookViewModel(int index)
        {
            return new EditBookViewModel(_library, _dashboardNavigationStore, CreateManageBooksViewModel, index);
        }
        private BorrowedBooksViewModel CreateBorrowedBooksViewModel()
        {
            return new BorrowedBooksViewModel(_library, _dashboardNavigationStore, CreateManageBooksViewModel);
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
