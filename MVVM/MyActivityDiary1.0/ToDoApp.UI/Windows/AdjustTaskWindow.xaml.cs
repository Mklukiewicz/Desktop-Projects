using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AdjustTaskWindow.xaml
    /// </summary>
    public partial class AdjustTaskWindow : Window
    {       
        public AdjustTaskWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TaskItemViewModel vm)
            {
                vm.BuiltTask = new TaskItem(
                    vm.TaskItemViewModelTitle,
                    vm.TaskItemViewModelDescription ?? "",
                    vm.TaskItemViewModelStartDate ?? DateTime.Today,
                    vm.TaskItemViewModelFinishDate ?? DateTime.Today,
                     false,
                    taskProgress: true,
                    progressMaxInt: vm.MaxProgress,
                    progressCurrentInt: vm.CurrentProgress,
                    progressString: vm.TaskStringProgress ?? string.Empty,
                    (TaskPriority)vm.Priority
                );
            }
                DialogResult = true;
        }
    }
}
