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
        public User User { get; set; }
        public Book Book { get; set; }

        public RequestedBook(User User, Book Book)
        {
            //ID = iD;
            this.User = User;
            this.Book = Book;
        }
    }
}
