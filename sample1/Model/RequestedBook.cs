using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class RequestedBook
    {
        //public string ID { get; set; }
        public User User { get; }
        public Book Book { get; }

        public RequestedBook(User user, Book book)
        {
            //ID = iD;
            User = user;
            Book = book;
        }
    }
}
