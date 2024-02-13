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
        public ulong CurrentId { get; set; }
        public Library() 
        {
            CurrentId = 0;
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
        public List<Book> SearchBookByString(string searchText)
        {
            return Books.Where(b => b.Match(searchText)).ToList();
        }
        public List<Book> SearchBookByGenre(Genre genre)
        {
            return Books.Where(b => b.Genre == genre).ToList();
        }
        public void BorrowBook(int index, User user, DateTime dateBorrowed)
        {
            Books[index].Status = BookStatus.Unavailable;
            BorrowedBooks.Add(new BorrowedBook(user, Books[index], Status.Borrowed, dateBorrowed));
        }
        public void ReturnBook(int index, DateTime dateReturned)
        {
            BorrowedBook book = BorrowedBooks[index];
            book.Status = Status.Returned;
            book.SetReturnDate(dateReturned);
            ulong getID = book.Book.ID;
            Books.FirstOrDefault(b => b.ID == getID).Status = BookStatus.Available;
        }
        public ulong GetCurrentIDAndIncrement()
        {
            CurrentId++;
            return CurrentId;
        }
    }
}
