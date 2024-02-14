using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class RequestedBook
    {
        public ulong ID { get; set; }
        public User User { get; }
        public Book Book { get; }

        public RequestedBook(ulong iD, User user, Book book)
        {
            ID = iD;
            User = user;
            Book = book;
        }
    }
}
