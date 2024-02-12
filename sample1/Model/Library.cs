using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class Library
    {
        public List<Book> Books { get; }
        public List<BorrowedBook> BorrowedBooks { get; }
        public Library() 
        {
            Books = new List<Book>();
            BorrowedBooks = new List<BorrowedBook>();
        }
        public void AddBook(Book book)
        {
            Books.Add(book);
        }
        public List<Book> GetBooks() 
        {
            return Books;
        }
    }
}
