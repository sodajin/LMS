using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Commands
{
    public class DiscardCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<List<Book>, string, ViewModelBase> _createManageBookViewModel;
        private readonly Func<List<User>, string, ViewModelBase> _createManageMemberViewModel;
        public DiscardCommand(NavigationStore navigationStore, Func<List<Book>, string, ViewModelBase> createManageBookViewModel) 
        {
            _navigationStore = navigationStore;
            _createManageBookViewModel = createManageBookViewModel;
        }

        public DiscardCommand(NavigationStore navigationStore, Func<List<User>, string, ViewModelBase> createManageMemberViewModel)
        {
            _navigationStore = navigationStore;
            _createManageMemberViewModel = createManageMemberViewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult logOutAnswer = MessageBox.Show("Discard changes?", "Cancel Changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (logOutAnswer == MessageBoxResult.No) 
            {
                return;
            }

            if (_createManageBookViewModel != null)
            {
                _navigationStore.CurrentViewModel = _createManageBookViewModel(new List<Book>(), "");
                return;
            }
            if (_createManageMemberViewModel != null)
            {
                _navigationStore.CurrentViewModel = _createManageMemberViewModel(new List<User>(), "");
                return;
            }
        }
    }
}
