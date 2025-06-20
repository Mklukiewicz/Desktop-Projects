﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public TaskItemViewModel SharedViewModel { get; } = new TaskItemViewModel();

        public MainPage()
        {
            InitializeComponent();

            DataContext = SharedViewModel;

            var homePage = new HomePage { DataContext = SharedViewModel };
            var listPage = new ListOfTasksPage { DataContext = SharedViewModel };
            var finishedTasksListPage = new FinishedTasksPage { DataContext = SharedViewModel };
            var calendarPage = new CalendarPage { DataContext = new CalendarViewModel(SharedViewModel.TaskItems) };
            

            HomeTab.Content = homePage;
            ListTab.Content = listPage;
            FinishedTasksTab.Content = finishedTasksListPage;
            CalendarTab.Content = calendarPage;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeTab.IsSelected)
            {
                if (DataContext is TaskItemViewModel vm)
                {
                    vm.UpdateMotivationalQuote();
                }
            }
        }
    }
}
