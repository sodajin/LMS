using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public enum Status
    {
        Returned,
        Borrowed,
        Overdue
    }
    public class BorrowedBook
    {
        public User User { get; }
        public Book Book { get; }
        public DateTime DateBorrowed { get; }
        public DateTime DateReturned { get; set;  }
        public Status Status { get; set; }   
        public BorrowedBook(
            User user, 
            Book book, 
            Status status,
            DateTime dateBorrowed
        ) {
            this.User = user;
            this.Book = book;
            this.Status = status;
            this.DateBorrowed = dateBorrowed;
            this.DateReturned = new DateTime(1, 1, 1);
        }
        public void SetReturnDate( DateTime date )
        {
            this.DateReturned = date;
        }

    }
}
