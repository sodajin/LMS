using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class Book
    {
        public ulong ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public string Publisher { get; }
        public DateTime PublishedDate { get; }
        public string Genre { get; }
        public Book(
            ulong isbn,
            string title,
            string author,
            string publisher,
            DateTime publishedDate,
            string genre
        ) {
            this.ISBN = isbn;
            this.Title = title;
            this.Author = author;
            this.Publisher = publisher;
            this.PublishedDate = publishedDate;
            this.Genre = genre;
        }
    }
}
