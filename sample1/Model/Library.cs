using LibraryManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class Library
    {
        public List<Book> Books { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; }
        public List<RequestedBook> RequestedBooks { get; }
        public ulong CurrentBookID { get; set; }
        public ulong CurrentBorrowedBookID { get; set; }
        DataContext dataContext = new DataContext();
        //public ulong CurrentRequestedBookID { get; set; }
        public Library()
        {
            CurrentBookID = 0;
            CurrentBorrowedBookID = 0;
            //CurrentRequestedBookID = 0;
            Books = new List<Book>();
            BorrowedBooks = new List<BorrowedBook>();
            RequestedBooks = new List<RequestedBook>();
        }
        public void AddBook(Book book)
        {
            Books.Add(book);
        }
        public List<Book> GetBooks()
        {
            return Books;
        }
        public Book GetBookByID(ulong bookID) //new
        {
            return Books.FirstOrDefault(u => u.ID == bookID);
        }
        public List<Book> GetBooksByGenre(Genre genre)
        {
            return Books.Where(b => b.Genre == genre).ToList();
        }
        public List<BorrowedBook> GetBorrowedBooks()
        {
            return BorrowedBooks.Where(b => b.Status == Status.Borrowed).ToList();
        }
        public List<BorrowedBook> GetBorrowedBooksByUser(User user)
        {
            return BorrowedBooks.Where(b => b.User == user).ToList();
        }
        public List<RequestedBook> GetRequestedBooks()
        {
            return RequestedBooks;
        }
        public Book GetBookFromElement(int index)
        {
            return Books.ElementAt(index);
        }
        public RequestedBook GetRequestedBookFromElement(int index)
        {
            return RequestedBooks.ElementAt(index);
        }
        public Book GetBookFromID(ulong ID)
        {
            return Books.Where(b => b.ID == ID).FirstOrDefault();
        }
        public BorrowedBook GetBorrowedBookFromID(ulong ID)
        {
            return BorrowedBooks.Where(b => b.ID == ID).FirstOrDefault();
        }
        public void ReplaceBook(Book newBook, ulong ID)
        {
            Trace.WriteLine(ID);
            Book book = GetBookFromID(ID);
            book.Replace(newBook);
        }
        public List<Book> SearchBookByString(string searchText)
        {
            return Books.Where(b => b.Match(searchText)).ToList();
        }
        public List<Book> SearchBookByGenre(Genre genre)
        {
            return Books.Where(b => b.Genre == genre).ToList();
        }
        public List<BorrowedBook> SearchBorrowedBookByString(string searchText)
        {
            return BorrowedBooks.Where(b => b.Match(searchText) && b.Status == Status.Borrowed).ToList();
        }
        public void RequestBook(Book book, User user)
        {
            RequestedBooks.Add(new RequestedBook(user, book));
        }
        public void RequestBook2(RequestedBook requestedbook) //new
        {
            RequestedBooks.Add(requestedbook);
        }
        public void AcceptRequestBook(RequestedBook requestedBook)
        {
            Book book = requestedBook.Book;
            User user = requestedBook.User;
            BorrowBook(book, user, DateTime.Today);
            RequestedBooks.Remove(requestedBook);
            dataContext.RemoveRequest(requestedBook);
        }
        public void DenyRequestBook(RequestedBook requestedBook)
        {
            RequestedBooks.Remove(requestedBook);
        }
        public void BorrowBook(Book book, User user, DateTime dateBorrowed)
        {
            //Book book = GetBookFromID(bookID);
            book.Status = BookStatus.Unavailable;
            dataContext.SetBookUnavailable(book);
            BorrowedBook borrowedBook = new BorrowedBook(GetCurrentBorrowedBookIDAndIncrement(), user, book, Status.Borrowed, dateBorrowed);
            BorrowedBooks.Add(borrowedBook);
            dataContext.SaveBorrowedBook(borrowedBook);
        }
        public void BorrowBook2(BorrowedBook borrowedBook)
        {
            BorrowedBooks.Add(borrowedBook);
            GetCurrentBorrowedBookIDAndIncrement();
        }
        public void ReturnBook(BorrowedBook book, DateTime dateReturned)
        {
            //BorrowedBook book = GetBorrowedBookFromID(bookID);
            book.Status = Status.Returned;
            book.SetReturnDate(dateReturned);
            dataContext.SetBookReturned(book, dateReturned);
            ulong getID = book.Book.ID;
            Books.FirstOrDefault(b => b.ID == getID).Status = BookStatus.Available;
        }
        public ulong GetCurrentBookIDAndIncrement()
        {
            CurrentBookID++;
            return CurrentBookID;
        }
        public ulong GetCurrentBorrowedBookIDAndIncrement()
        {
            CurrentBorrowedBookID++;
            return CurrentBorrowedBookID;
        }
        //public ulong GetCurrentRequestedBookIDAndIncrement()
        //{
        //    CurrentRequestedBookID++;
        //    return CurrentRequestedBookID;
        //}
    }
}
