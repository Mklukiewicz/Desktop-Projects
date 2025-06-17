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

namespace ToDoApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for UpdateTaskWindow.xaml
    /// </summary>
    public partial class UpdateTaskWindow : Window
    {
        public UpdateTaskWindow()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TaskItem task)
            {              
                DialogResult = true;
                Close();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
