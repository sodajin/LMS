using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagementSystem.Data;

namespace sample1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NavigationStore _navigationStore;
        private Library _library;
        private UserList _users;

        public App()
        {
            _navigationStore = new NavigationStore();
            _library = new Library();
            _users = new UserList();
            List<User> members = new List<User>();
            List<Book> books = new List<Book>();
            List<RequestedBook> requestedBooks = new List<RequestedBook>();
            List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();
            DataContext dataContext = new DataContext();

            members = dataContext.LoadMembers();

            foreach (User user in members)
            {
                _users.SignUp(user);
            }

            books = dataContext.LoadBooks();

            foreach (Book book in books)
            {
                _library.AddBook(book);
                _library.GetCurrentBookIDAndIncrement();
            }

            requestedBooks = dataContext.LoadRequests(_library, _users);

            foreach (RequestedBook requestedBook in requestedBooks)
            {
                _library.RequestBook2(requestedBook);
            }

            borrowedBooks = dataContext.LoadBorrowedBooks(_library, _users);

            foreach (BorrowedBook borrowedBook in borrowedBooks)
            {
                _library.BorrowBook2(borrowedBook);
            }

            //_library.RequestBook(_library.Books[1], _users.Users[0]);
            //_library.BorrowBook(_library.Books[1], _users.Users[0], new DateTime(2023, 2, 14));
            //_library.BorrowBook(_library.Books[0], _users.Users[0], new DateTime(2023, 2, 14));
            //_library.BorrowBook(_library.Books[1], _users.Users[0], new DateTime(2023, 2, 14));
            //_library.ReturnBook(1, DateTime.Today);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateLogInViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private LogInViewModel CreateLogInViewModel()
        {
            return new LogInViewModel(_navigationStore, _users, CreateMemberDashboardViewModel, CreateAdminDashboardViewModel);
        }

        private MemberDashboardViewModel CreateMemberDashboardViewModel(User user)
        {
            return new MemberDashboardViewModel(user, _navigationStore, CreateLogInViewModel, _library);
        }

        private AdminDashboardViewModel CreateAdminDashboardViewModel(User user)
        {
            return new AdminDashboardViewModel(user, _navigationStore, CreateLogInViewModel, _library, _users);
        }
    }
}
