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
        public List<BorrowedBook> GetBorrowedBooks()
        {
            return BorrowedBooks;
        }
        public Book GetBookFromElement(int index)
        {
            return Books.ElementAt(index);
        }
        public void ReplaceBook(Book newBook, int index)
        {
            Books[index] = newBook;
        }
        public List<Book> SearchBook(string searchText)
        {
            List<Book> results = new List<Book>();

            foreach (Book book in Books) 
            {
                if (book.Match(searchText)) results.Add(book);
            }
            
            return results;
        }
    }
}
