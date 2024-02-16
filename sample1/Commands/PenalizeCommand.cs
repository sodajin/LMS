using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Commands
{
    public class PenalizeCommand : CommandBase
    {
        private readonly ManageMembersViewModel _viewModel;
        private readonly UserList _userList;
        private readonly int _index;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<User>, string, ViewModelBase> _createManageMembersViewModel;

        public PenalizeCommand(ManageMembersViewModel viewModel, UserList userList, int index, NavigationStore dashboardNavigationStore, Func<List<User>, string, ViewModelBase> createManageMembersViewModel) 
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
                MessageBox.Show("Please select a member on the table to penalize.", "No selected member", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;     
            }

            User user = _userList.GetUserFromID(_viewModel.MemberTable.ElementAt(_index).MemberID);
            if (user.Reputation <= 0)
            {
                MessageBox.Show("No more reputation points to deduct.", "Low penalty warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you wish to penalize this user?", "Penalize User", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) { return; }

            user.AddReputation(-20);

            DataContext dataContext = new DataContext();
            dataContext.PenalizeMember(user);

            _dashboardNavigationStore.CurrentViewModel = _createManageMembersViewModel(new List<User>(), "");
        }
    }
}
