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

            _library.AddBook(new Book(
                9784091273437,
                "Kimetsu no Yaiba, Vol. 1",
                "Koyoharu Gotouge",
                "VIZ Media LLC",
                new DateTime(2016, 2, 15),
                Genre.Arts,
                BookStatus.Available
                )
            );
            _library.AddBook(new Book(
                9781974700523,
                "Komi-san Can't Communicate",
                "Tomohito Oda",
                "Shogakukan",
                new DateTime(2016, 9, 16),
                Genre.Arts,
                BookStatus.Available
                )
            );

            _users.SignUp(new User(
                "2021-10945-MN-0",
                "Genesis",
                "Cornista",
                "Lovino",
                "lezzthanthree",
                "password",
                AccountType.Admin
                ));
            _users.SignUp(new User(
                "a",
                "Anthonette",
                "Villafuente",
                "Macapanas",
                "a",
                "a",
                AccountType.Admin
                ));
            _users.SignUp(new User(
                "2021-10946-MN-0",
                "Smilie",
                "",
                "Pop",
                "smiliep",
                "password",
                AccountType.Simple
                ));
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

        private MemberDashboardViewModel CreateMemberDashboardViewModel()
        {
            return new MemberDashboardViewModel(_navigationStore, CreateLogInViewModel, _library);
        }

        private AdminDashboardViewModel CreateAdminDashboardViewModel(User user) 
        {
            return new AdminDashboardViewModel(user, _navigationStore, CreateLogInViewModel, _library);
        }
    }
}
