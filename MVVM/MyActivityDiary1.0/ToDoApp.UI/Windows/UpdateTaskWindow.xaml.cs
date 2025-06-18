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
using static ToDoApp.Core.Models.TaskItem;
using ToDoApp.UI.ViewModels;

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
            if (DataContext is TaskItemViewModel vm && vm.BuiltTask is TaskItem task)
            {
                task.Title = vm.TaskItemViewModelTitle;
                task.Description = vm.TaskItemViewModelDescription;
                task.StartDate = vm.TaskItemViewModelStartDate ?? DateTime.Now;

                if (vm.HasTaskProgress)
                {
                    task.TaskProgress = true;
                    task.FinishDate = vm.TaskItemViewModelFinishDate;
                    task.ProgressCurrentInt = vm.CurrentProgress;
                    task.ProgressMaxInt = vm.MaxProgress;
                    task.ProgressString = vm.TaskStringProgress;
                }
                else
                {
                    task.TaskProgress = false;
                    task.FinishDate = null;
                    task.ProgressCurrentInt = 0;
                    task.ProgressMaxInt = 0;
                    task.ProgressString = vm.TaskStringProgress;
                }

                task.Priority = (TaskPriority)vm.Priority;

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
