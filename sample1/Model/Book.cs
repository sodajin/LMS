using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public enum Genre
    {
        GeneralWorks,
        Philosophy,
        Religion,
        SocialScience,
        Language,
        Science,
        Technology,
        Arts,
        Literature,
        HistoryAndGeography
    }
    public enum BookStatus
    {
        ForViewing,
        Available,
        Unavailable
    }
    public class Book
    {
        public ulong ID { get; set;  }
        public ulong ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set;  }
        public string Publisher { get; set;  }
        public DateTime PublishedDate { get; set; }
        public Genre Genre { get; set; }
        public BookStatus Status { get; set; }
        public Book (
            ulong ID,
            ulong isbn,
            string title,
            string author,
            string publisher,
            DateTime publishedDate,
            Genre genre,
            BookStatus status
        ) {
            this.ID = ID;
            this.ISBN = isbn;
            this.Title = title;
            this.Author = author;
            this.Publisher = publisher;
            this.PublishedDate = publishedDate;
            this.Genre = genre;
            this.Status = status;
        }

        public override string ToString()
        {
            return this.Title;
        }

        public bool Match(string searchTest)
        {
            if (this.Title.ToLower().Contains(searchTest.ToLower()) ||
                this.Author.ToLower().Contains(searchTest.ToLower())) return true;
            return false;
        }
        public void Replace(Book newBook)
        {
            this.ISBN = newBook.ISBN;
            this.Title = newBook.Title;
            this.Author = newBook.Author;
            this.Publisher = newBook.Publisher;
            this.PublishedDate = newBook.PublishedDate;
            this.Genre = newBook.Genre;
            this.Status = newBook.Status;
        }
    }
}
