using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using LibraryManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.Commands
{
    public class RequestBookCommand : CommandBase
    {
        private readonly Library _library;
        private readonly Book _book;
        private readonly User _user;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<Book>, string, ViewModelBase> _createSearchBookViewModel;

        public RequestBookCommand(Library library, Book book, User user, NavigationStore dashboardNavigationStore, Func<List<Book>, string, ViewModelBase> createSearchBookViewModel)
        {
            _library = library;
            _book = book;
            _user = user;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createSearchBookViewModel = createSearchBookViewModel;
        }
        public override void Execute(object parameter)
        {
            if (_book.Status == BookStatus.Unavailable)
            {
                MessageBox.Show("The book is currently unavailble for requesting.", "Book unavailable", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"You are requesting {_book.Title} to borrow. Continue?", "Request Book", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) 
            {
                return;
            }

            _library.RequestBook(_book, _user);

            DataContext dataContext = new DataContext();
            dataContext.RequestBook(_book, _user);

            MessageBox.Show($"The book has been requested. Please head to front desk.", "Request Book", MessageBoxButton.OK, MessageBoxImage.Information);
            _dashboardNavigationStore.CurrentViewModel = _createSearchBookViewModel(new List<Book>(), "");
        }
    }
}
