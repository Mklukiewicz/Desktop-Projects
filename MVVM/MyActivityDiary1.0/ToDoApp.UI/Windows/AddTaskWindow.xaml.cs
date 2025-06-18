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
using static ToDoApp.Core.Models.TaskItem;

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
            if (DataContext is TaskItemViewModel vm)
            {
                if (vm.HasTaskProgress == true)
                {
                    var adjustWindow = new AdjustTaskWindow
                    {
                        DataContext = vm,
                        Owner = this
                    };
                    if (adjustWindow.ShowDialog() == true && vm.BuiltTask != null)
                    {
                        DialogResult = true;
                        Close();
                    }
                }
                else
                {
                    vm.BuiltTask = new TaskItem(
                        vm.TaskItemViewModelTitle,
                        vm.TaskItemViewModelDescription ?? "",
                        vm.TaskItemViewModelStartDate ?? DateTime.Today,
                        false,
                        (TaskPriority)vm.Priority
                    );

                    DialogResult = true;
                    Close();
                }
            }            
        }
    }
}
