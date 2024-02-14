using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Commands
{
    public class ClearPenaltyCommand : CommandBase
    {
        private readonly ManageMembersViewModel _viewModel;
        private readonly UserList _userList;
        private readonly int _index;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<User>, string, ViewModelBase> _createManageMembersViewModel;

        public ClearPenaltyCommand(ManageMembersViewModel viewModel, UserList userList, int index, NavigationStore dashboardNavigationStore, Func<List<User>, string, ViewModelBase> createManageMembersViewModel) 
        {
            _viewModel = viewModel;
            _userList = userList;
            _index = index;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createManageMembersViewModel = createManageMembersViewModel;
        }
        public override void Execute(object parameter)
        {
            if (_index < 0)
            {
                MessageBox.Show("Please select a member on the table to clear their penalty.", "No selected member", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;     
            }

            User user = _userList.GetUserFromID(_viewModel.MemberTable.ElementAt(_index).MemberID);
            if (user.Reputation >= 100)
            {
                MessageBox.Show("Credit score on highest.", "High Credit Score", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you wish to clear penalties on this user?", "Clear Penalty", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) { return; }

            user.SetReputation(100);

            _dashboardNavigationStore.CurrentViewModel = _createManageMembersViewModel(new List<User>(), "");
        }
    }
}
