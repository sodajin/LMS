using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Commands
{
    public class DiscardCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createManageBooksViewModel;
        public DiscardCommand(NavigationStore navigationStore, Func<ViewModelBase> createManageBooksViewModel) 
        {
            _navigationStore = navigationStore;
            _createManageBooksViewModel = createManageBooksViewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult logOutAnswer = MessageBox.Show("Discard changes?", "Cancel Changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (logOutAnswer == MessageBoxResult.No) 
            {
                return;
            }

            _navigationStore.CurrentViewModel = _createManageBooksViewModel();
        }
    }
}
