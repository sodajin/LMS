using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data
{
    public class BorrowedBookDTO
    {
        public int IDBook { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public Genre Genre { get; set; }
        public BookStatus Status { get; set; }
        public string IDUser { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string Password { get; }
        public int Reputation { get; set; }
        public AccountType AccountType { get; }
    }
}
