using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.Commands
{
    public class SelectEditBookCommand : CommandBase
    {
        public int _id;
        private readonly Library _library;
        private readonly NavigationStore _navigationStore;
        private readonly Func<int, ViewModelBase> _createEditBookViewModel;
        public SelectEditBookCommand(ManageBooksViewModel viewModel, Library library, int ID, NavigationStore navigationStore, Func<int, ViewModelBase> createEditBookViewModel) 
        {
            _library = library;
            _id = ID;
            _navigationStore = navigationStore;
            _createEditBookViewModel = createEditBookViewModel;
        }
        public override void Execute(object parameter)
        {

            if (_id <= 0)
            {
                MessageBox.Show($"Please select a book from the table." , "No book selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Book book = _library.GetBookFromID(_id);
            if (book.Status == BookStatus.Unavailable) 
            {
                MessageBox.Show("Please select a book that is available.", "Invalid book selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _navigationStore.CurrentViewModel = _createEditBookViewModel(_id);
        }
    }
}
