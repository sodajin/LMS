using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;
using LibraryManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Commands
{
    public class AddMemberCommand : CommandBase
    {
        private readonly AddMemberViewModel _viewModel;
        private readonly UserList _userList;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<User>, string, ViewModelBase> _createManageMemberViewModel;

        public AddMemberCommand(
            AddMemberViewModel viewModel,
            UserList userList,
            NavigationStore dashboardNavigationStore,
            Func<List<User>, string, ViewModelBase> createManageMemberViewModel
        ) {
            _viewModel = viewModel;
            _userList = userList;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createManageMemberViewModel = createManageMemberViewModel;
        }
        public override void Execute(object parameter)
        {
            User newUser = new User(
                _viewModel.ID,    
                _viewModel.FirstName,
                _viewModel.MiddleName,
                _viewModel.LastName,
                _viewModel.Username,
                _viewModel.Password,
                100, AccountType.Simple
            );

            DataContext dataContext = new DataContext();
            dataContext.SaveMember( newUser );

            _userList.SignUp( newUser );
            _dashboardNavigationStore.CurrentViewModel = _createManageMemberViewModel(new List<User>(), "");
        }
    }
}
