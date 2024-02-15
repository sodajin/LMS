﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Store;

namespace LibraryManagementSystem.ViewModel
{
    public class AddBookViewModel : ViewModelBase
    {
        private readonly Library _library;
        private readonly NavigationStore _dashboardNavigationStore;
        private readonly Func<List<Book>, string, ViewModelBase> _createManageBookViewModel;

        private string _ISBN;
        public string ISBN
        {
            get => _ISBN;
            set
            {
                _ISBN = value;
                OnPropertyChanged(nameof(_ISBN));
            }
        }

        private string _title;
        public string Title 
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(_title));
            }
        }

        private string _author;
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(_author));
            }
        }

        private string _publisher;
        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(_publisher));
            }
        }

        private DateTime _datePublished;
        public DateTime DatePublished 
        {
            get => _datePublished;
            set
            {
                _datePublished = value;
                OnPropertyChanged(nameof(DatePublished));
            }
        }

        private Genre _genre;
        public Genre Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(_genre));
            }
        }

        public ObservableCollection<Genre> GenreItems => new ObservableCollection<Genre>(Enum.GetValues(typeof(Genre)).Cast<Genre>());

        private BookStatus _availability;
        public BookStatus Availability
        {
            get => _availability;
            set
            {
                _availability = value;
                OnPropertyChanged(nameof(Availability));
            }
        }

        public ObservableCollection<BookStatus> BookStatuses => new ObservableCollection<BookStatus>(Enum.GetValues(typeof(BookStatus)).Cast<BookStatus>());

        public ICommand AddBookCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public AddBookViewModel(Library library, NavigationStore dashboardNavigationStore, Func<List<Book>, string, ViewModelBase> createManageBookViewModel)
        {
            _library = library;
            _dashboardNavigationStore = dashboardNavigationStore;
            _createManageBookViewModel = createManageBookViewModel;
            AddBookCommand = new AddBookCommand(this, _library, _dashboardNavigationStore, _createManageBookViewModel);
            CancelCommand = new DiscardCommand(_dashboardNavigationStore, _createManageBookViewModel);
        }
    }
}
