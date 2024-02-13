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
        public string MemberID => _borrowedBooks.User.GetID();
        public string MemberFullName => _borrowedBooks.User.GetFullName();
        public string BookTitle => _borrowedBooks.Book.Title;
        public DateTime DateBorrowed => _borrowedBooks.DateBorrowed;
        public DateTime DateReturned => _borrowedBooks.DateReturned;

        public BorrowedBooksTableViewModel(BorrowedBook borrowedBooks) 
        {
            _borrowedBooks = borrowedBooks;
        }
    }
}
