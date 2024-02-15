using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Commands
{
    public class AcceptRequestCommand : CommandBase
    {
        private readonly Library _library;
        private readonly RequestsViewModel _viewModel;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<ViewModelBase> _createRequestViewModel;
        public AcceptRequestCommand(Library library, RequestsViewModel viewModel, NavigationStore dashboardNavigationStore, Func<ViewModelBase> createRequestViewModel)
        {
            _library = library;
            _viewModel = viewModel;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createRequestViewModel = createRequestViewModel;
        }
        public override void Execute(object parameter)
        {
            if (_viewModel.SelectIndex < 0)
            {
                MessageBox.Show($"Please select a book from the table.", "No book selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RequestedBook requestedBook = _library.GetRequestedBookFromElement(_viewModel.SelectIndex);

            if (requestedBook.Book.Status == BookStatus.Unavailable) 
            {
                MessageBox.Show($"The book is currently borrowed or unavailable.", "Book unavailable", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _library.AcceptRequestBook(requestedBook);

            MessageBox.Show("Requested book accepted");

            _dashboardNavigationStore.CurrentViewModel = _createRequestViewModel();
        }
    }
}
