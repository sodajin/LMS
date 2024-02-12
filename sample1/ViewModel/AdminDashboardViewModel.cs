using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.Commands;

namespace LibraryManagementSystem.ViewModel
{
    public class AdminDashboardViewModel : ViewModelBase
    {
        public string Name;

        private readonly NavigationStore _navigationStore;

        private NavigationStore _dashboardNavigationStore;
        public ViewModelBase CurrentDashboardViewModel => _dashboardNavigationStore.CurrentViewModel;

        public ICommand SearchBooksCommand { get; } 
        public ICommand RequestsCommand { get; }
        public ICommand ManageBooksCommand { get; }
        public ICommand ManageMembersCommand { get; }
        public ICommand LogOutCommand { get; }

        public AdminDashboardViewModel(NavigationStore navigationStore, Func<ViewModelBase> createLogInViewModel)
        {
            _navigationStore = navigationStore;
            _dashboardNavigationStore = new NavigationStore();
            _dashboardNavigationStore.CurrentViewModel = CreateSearchBooksViewModel();
            _dashboardNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            SearchBooksCommand = new NavigateCommand(_dashboardNavigationStore, CreateSearchBooksViewModel);
            RequestsCommand = new NavigateCommand(_dashboardNavigationStore, CreateRequestsViewModel);
            ManageBooksCommand = new NavigateCommand(_dashboardNavigationStore, CreateManageBooksViewModel);
            ManageMembersCommand = new NavigateCommand(_dashboardNavigationStore, CreateManageMembersViewModel);
            LogOutCommand = new LogOutCommand(_navigationStore, createLogInViewModel);
        }

        private SearchBooksViewModel CreateSearchBooksViewModel()
        {
            return new SearchBooksViewModel();
        }
        private RequestsViewModel CreateRequestsViewModel()
        {
            return new RequestsViewModel();
        }
        private ManageBooksViewModel CreateManageBooksViewModel()
        {
            return new ManageBooksViewModel();
        }
        private ManageMembersViewModel CreateManageMembersViewModel()
        {
            return new ManageMembersViewModel();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentDashboardViewModel));
        }
    }
}
