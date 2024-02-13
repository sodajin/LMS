using System;
using System.Collections.Generic;
using System.Linq;
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
        public ulong ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public string Publisher { get; }
        public DateTime PublishedDate { get; }
        public Genre Genre { get; }
        public BookStatus Status { get; set; }
        public Book (
            ulong isbn,
            string title,
            string author,
            string publisher,
            DateTime publishedDate,
            Genre genre,
            BookStatus status
        ) {
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
    }
}
