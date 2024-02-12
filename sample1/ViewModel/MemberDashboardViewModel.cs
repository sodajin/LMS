using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class MemberDashboardViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Library _library;

        public MemberDashboardViewModel(NavigationStore navigationStore, Func<ViewModelBase> createLogInViewModel, Library library) 
        {
            _navigationStore = navigationStore;
            _library = library;
        }
    }
}
