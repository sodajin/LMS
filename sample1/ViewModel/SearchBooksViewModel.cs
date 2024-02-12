using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class SearchBooksViewModel : ViewModelBase
    {
        public Library _library;
        private List<Book> _books => _library.GetBooks();
        private readonly ObservableCollection<BookTableViewModel> _bookTable;

        public IEnumerable<BookTableViewModel> BookTable => _bookTable;

        public SearchBooksViewModel(Library books)
        {
            _library = books;
            _bookTable = new ObservableCollection<BookTableViewModel>();
            IEnumerable<Book> IBookEnumerable = _books;
            IEnumerator<Book> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new BookTableViewModel(book_enumerate.Current));
            }
        }
    }
}
