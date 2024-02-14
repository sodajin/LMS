using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class BookTableViewModel : ViewModelBase
    {
        public readonly Book _book;
        public ulong ID => _book.ID;
        public ulong ISBN => _book.ISBN;
        public string Title => _book.Title;
        public string Author => _book.Author;
        public string Publisher => _book.Publisher;
        public DateTime PublishedDate => _book.PublishedDate;
        public int PublishedYear => _book.PublishedDate.Year;
        public string Genre => _book.Genre.ToString();
        public string Status => _book.Status.ToString();

        public BookTableViewModel(Book book) 
        {
            _book = book;
        }
    }
}
