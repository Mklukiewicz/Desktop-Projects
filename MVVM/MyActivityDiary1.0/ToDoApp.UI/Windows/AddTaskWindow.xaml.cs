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
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public TaskItemViewModel ViewModel { get; }
        public AddTaskWindow()
        {
            InitializeComponent();    
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as TaskItemViewModel;

            if (vm.HasTaskProgress == true)
            {
                var adjust = new AdjustTaskWindow()
                {
                    DataContext = vm
                };
                adjust.ShowDialog();
            }
            else
            {
                DialogResult = true;
                Close();
            }
             
        }
    }
}
