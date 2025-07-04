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
using ToDoApp.DB.Repositories.Interfaces;
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for ListOfTasksPage.xaml
    /// </summary>
    public partial class ListOfTasksPage : UserControl
    {
        private readonly ITaskItemRepository _repository;
        public ListOfTasksPage(TaskItemViewModel sharedViewModel)
        {
            InitializeComponent();

            DataContext = sharedViewModel;
        }
    }
}
