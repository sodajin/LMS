﻿using LibraryManagementSystem.Store;
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

        public App()
        {
            _navigationStore = new NavigationStore();   
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
            return new LogInViewModel(_navigationStore, CreateAdminDashboardViewModel);
        }

        private AdminDashboardViewModel CreateAdminDashboardViewModel() 
        {
            return new AdminDashboardViewModel(_navigationStore, CreateLogInViewModel);
        }
    }
}
