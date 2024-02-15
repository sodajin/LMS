using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class MemberDashboardViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Library _library;
        private readonly User _user;
        private NavigationStore _dashboardNavigationStore;
        public ViewModelBase CurrentDashboardViewModel => _dashboardNavigationStore.CurrentViewModel;

        public string Name => _user.FirstName;

        public ICommand BrowseBookCommand { get; }
        public ICommand ViewAccountCommand { get; }
        public ICommand LogOutCommand { get; }

        public MemberDashboardViewModel(User user, NavigationStore navigationStore, Func<ViewModelBase> createLogInViewModel, Library library)
        {
            _user = user;
            _navigationStore = navigationStore;
            _library = library;
            _dashboardNavigationStore = new NavigationStore();
            _dashboardNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            BrowseBookCommand = new NavigateSearchBooksCommand(_dashboardNavigationStore, new List<Book>(), "", CreateSearchBooksViewModel);
            ViewAccountCommand = new NavigateCommand(_dashboardNavigationStore, CreateViewAccountViewModel);
            LogOutCommand = new LogOutCommand(_navigationStore, createLogInViewModel);
        }

        private SearchBooksViewModel CreateSearchBooksViewModel(List<Book> results, string searchText)
        {
           return new SearchBooksViewModel(_library, results, searchText, _dashboardNavigationStore, CreateSearchBooksViewModel, CreateBookViewModel);
        }
        private ViewAccountViewModel CreateViewAccountViewModel()
        {
            return new ViewAccountViewModel(_user, _library);
        }
        private BookViewModel CreateBookViewModel(Book book, List<Book> recentResults, string recentSearchText)
        {
            return new BookViewModel(_library, book, _user, _dashboardNavigationStore, recentResults, recentSearchText, CreateSearchBooksViewModel);
        }
        private void OnCurrentViewModelChanged()
       {
           OnPropertyChanged(nameof(CurrentDashboardViewModel));
       }
    }
}
