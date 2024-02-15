using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        private readonly Library _library;
        private readonly Book _book;
        private readonly User _user;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<Book>, string, ViewModelBase> _createSearchBookViewModel;

        public string ISBN => _book.ISBN.ToString();
        public string Title => _book.Title;
        public string Author => _book.Author;
        public string Publisher => _book.Publisher;
        public string DatePublished => _book.PublishedDate.ToString("d");
        public string Genre => _book.Genre.ToString();
        public string Status => _book.Status.ToString();

        public ICommand RequestBookCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        public BookViewModel(Library library, Book book, User user, NavigationStore dashboardNavigationStore, List<Book> recentResults, string recentSearchText, Func<List<Book>, string, ViewModelBase> createSearchBookViewModel) 
        {
            _library = library;
            _book = book;
            _user = user;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createSearchBookViewModel = createSearchBookViewModel;
            

            RequestBookCommand = new RequestBookCommand(_library, _book, _user, _dashboardNavigationStore, _createSearchBookViewModel);
            ReturnCommand = new NavigateSearchBooksCommand(_dashboardNavigationStore, recentResults, recentSearchText, _createSearchBookViewModel);
        }
    }
}
