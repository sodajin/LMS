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
    }
    public class BorrowedBook
    {
        public int ID { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime DateReturned { get; set;  }
        public Status Status { get; set; }   
        public BorrowedBook(
            int ID,
            User User, 
            Book Book, 
            Status Status,
            DateTime DateBorrowed
        ) {
            this.ID = ID;
            this.User = User;
            this.Book = Book;
            this.Status = Status;
            this.DateBorrowed = DateBorrowed;
            this.DateReturned = new DateTime(1, 1, 1);
        }
        public void SetReturnDate( DateTime date )
        {
            this.DateReturned = date;
        }
        public bool Match(string searchText)
        {
            if (this.User.GetFullName().ToLower().Contains(searchText.ToLower()) ||
                this.Book.ToString().ToLower().Contains(searchText.ToLower())) return true;
            return false;
        }

    }
}
