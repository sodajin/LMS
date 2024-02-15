using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.Commands
{
    public class ReturnBookCommand : CommandBase
    {
        private readonly Library _library;
        private readonly BorrowedBooksViewModel _viewModel;
        private readonly int _id;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<BorrowedBook>, string, ViewModelBase> _createSearchBorrowedBookViewModel;

        public ReturnBookCommand(Library library, BorrowedBooksViewModel viewModel, int id, NavigationStore dashboardNavigationStore, Func<List<BorrowedBook>, string, ViewModelBase> createSearchBorrowedBookViewModel)
        {
            _library = library;
            _viewModel = viewModel;
            _id = id;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createSearchBorrowedBookViewModel = createSearchBorrowedBookViewModel;
        }
        public override void Execute(object parameter)
        {
            if (_id <= 0)
            {
                MessageBox.Show($"Please select a book from the table.", "No book selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            MessageBoxResult result = MessageBox.Show("This book will be returned. Confirm?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) { return; }

            BorrowedBook book = _library.GetBorrowedBookFromID(_viewModel.BookTable.ElementAt(_viewModel.SelectIndex).ID);
            _library.ReturnBook(book, DateTime.Now);

            _dashboardNavigationStore.CurrentViewModel = _createSearchBorrowedBookViewModel(new List<BorrowedBook>(), "");
        }
    }
}
