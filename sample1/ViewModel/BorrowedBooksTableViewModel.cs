using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class BorrowedBooksTableViewModel : ViewModelBase
    {
        public readonly BorrowedBook _borrowedBooks;
        public int ID => _borrowedBooks.ID;
        public string MemberID => _borrowedBooks.User.ID;
        public string MemberFullName => _borrowedBooks.User.GetFullName();
        public string BookTitle => _borrowedBooks.Book.Title;
        public string DateBorrowed => _borrowedBooks.DateBorrowed.ToString("d");
        public string DateDue => _borrowedBooks.DateBorrowed.AddDays(3).ToString("d");
        public string DateReturned => _borrowedBooks.DateReturned.Equals(new DateTime(1, 1, 1)) ? "" : _borrowedBooks.DateReturned.ToString("d");

        public BorrowedBooksTableViewModel(BorrowedBook borrowedBooks)
        {
            _borrowedBooks = borrowedBooks;
        }
    }
}
