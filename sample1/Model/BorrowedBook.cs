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
        public int ID { get; }
        public User User { get; }
        public Book Book { get; }
        public DateTime DateBorrowed { get; }
        public DateTime DateReturned { get; set;  }
        public Status Status { get; set; }   
        public BorrowedBook(
            int ID,
            User user, 
            Book book, 
            Status status,
            DateTime dateBorrowed
        ) {
            this.ID = ID;
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
        public bool Match(string searchText)
        {
            if (this.User.GetFullName().ToLower().Contains(searchText.ToLower()) ||
                this.Book.ToString().ToLower().Contains(searchText.ToLower())) return true;
            return false;
        }

    }
}
