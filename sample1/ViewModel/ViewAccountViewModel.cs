using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class ViewAccountViewModel : ViewModelBase
    {
        public Library _library;
        public readonly User _user;
        private List<BorrowedBook> _books => _library.GetBorrowedBooksByUser(_user);
        private readonly ObservableCollection<BorrowedBooksTableViewModel> _bookTable;

        public IEnumerable<BorrowedBooksTableViewModel> BookTable => _bookTable;


        public string Name => _user.GetFullName();
        public string Username => _user.Username;
        public int Reputation => _user.Reputation;

        public ViewAccountViewModel(User user, Library library) 
        {
            _user = user;
            _library = library;

            _bookTable = new ObservableCollection<BorrowedBooksTableViewModel>();
            IEnumerable<BorrowedBook> IBookEnumerable = _books;
            IEnumerator<BorrowedBook> book_enumerate = IBookEnumerable.GetEnumerator();

            while (book_enumerate.MoveNext())
            {
                _bookTable.Add(new BorrowedBooksTableViewModel(book_enumerate.Current));
            }
        }
    }
}
