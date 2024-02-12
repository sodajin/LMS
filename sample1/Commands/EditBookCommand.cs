using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.Commands
{
    public class EditBookCommand : CommandBase
    {
        public int _index;
        private readonly List<Book> _books;
        private readonly NavigationStore _navigationStore;
        public EditBookCommand(int index, List<Book> books, NavigationStore navigationStore) 
        {
            _index = index;
            Trace.WriteLine($"In Edit Book Command: {_index}");
            _books = books;
            _navigationStore = navigationStore;
        }
        public override void Execute(object parameter)
        {
            Trace.WriteLine($"Button Clicked: {_index}");

            if (_index < 0)
            {
                MessageBox.Show($"Please select a book from the table. {_index}" , "No book selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Book book = _books.ElementAt(_index);
            if (book.Status == BookStatus.Unavailable) 
            {
                MessageBox.Show("Please select a book that is available.", "Invalid book selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // TODO: Switch to Edit Book View Model
        }
    }
}
