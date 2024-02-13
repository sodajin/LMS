using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Commands
{
    public class EditBookCommand : CommandBase
    {
        private readonly EditBookViewModel _viewModel;
        private readonly Library _library;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<ViewModelBase> _createManageBookViewModel;
        private readonly int _index;

        public EditBookCommand(
            EditBookViewModel viewModel, 
            Library library, 
            NavigationStore dashboardNavigationStore,
            Func<ViewModelBase> createManageBookViewModel,
            int index
        ) {
            _viewModel = viewModel;
            _library = library;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createManageBookViewModel = createManageBookViewModel;
            _index = index;
        }
        public override void Execute(object parameter)
        {
            if (CheckData() == false)
            {
                MessageBox.Show("Please input necessary data.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Book modifiedBook = new Book(
                _library.GetBookFromElement(_index).ID,
                _viewModel.ISBN,
                _viewModel.Title,
                _viewModel.Author,
                _viewModel.Publisher,
                _viewModel.DatePublished,
                _viewModel.Genre,
                _viewModel.Availability
            );

            _library.ReplaceBook(modifiedBook, _index);

            MessageBox.Show("Edit Book Complete");
            _dashboardNavigationStore.CurrentViewModel = _createManageBookViewModel();
        }
        public bool CheckData()
        {
            if (_viewModel.ISBN == 0 ||
            _viewModel.Title == null || _viewModel.Title == "" ||
            _viewModel.Author == null || _viewModel.Author == "" ||
            _viewModel.Publisher == null || _viewModel.Publisher == "" ||
            _viewModel.DatePublished == null)
            {
                return false;
            }
            return true;
        }
    }
}
