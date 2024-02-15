using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class RequestedBooksTableViewModel : ViewModelBase
    {
        public readonly RequestedBook _requestedBooks;
        //public ulong ID => _requestedBooks.ID;
        public string MemberID => _requestedBooks.User.ID;
        public string MemberFullName => _requestedBooks.User.GetFullName();
        public int MemberReputation => _requestedBooks.User.Reputation;
        public string BookTitle => _requestedBooks.Book.Title;

        public RequestedBooksTableViewModel(RequestedBook requestedBooks)
        {
            _requestedBooks = requestedBooks;
        }
    }
}


