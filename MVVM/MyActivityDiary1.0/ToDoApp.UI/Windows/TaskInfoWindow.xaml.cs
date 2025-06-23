using System;
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
using System.Windows.Shapes;
using ToDoApp.Core.Models;
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI.Windows//Prawdopodobnie do usunięcia
{
    /// <summary>
    /// Interaction logic for TaskInfoWindow.xaml
    /// </summary>
    public partial class TaskInfoWindow : Window
    {
        public TaskItem? CurrentTaskItem { get; set; }

        public TaskItemViewModel? ParentViewModel { get; set; }

        public TaskInfoWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }       
    }
}
