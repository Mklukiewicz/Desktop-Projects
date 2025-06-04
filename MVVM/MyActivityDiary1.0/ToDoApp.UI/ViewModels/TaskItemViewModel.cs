using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Core.Helpers;
using ToDoApp.Core.Models;

namespace ToDoApp.UI.ViewModels
{
    public class TaskItemViewModel
    {
        public string TaskItemViewModelTitle { get; set; }
        public string? TaskItemViewModelDescription { get; set; }
        public DateTime TaskItemViewModelDueTime { get; set; }
        public bool TaskItemViewModelIsChecked { get; set; }
      
        public TaskItemViewModel()
        {
            AddNewTaskItemCommand = new RelayCommand(AddNewTask);
            DeleteTaskItemCommand = new RelayCommand(DeleteTaskItem);
        }
        public ObservableCollection<TaskItem> TaskItems { get; set; } = new ObservableCollection<TaskItem>();

        public  ICommand AddNewTaskItemCommand { get; set; }
        public  ICommand DeleteTaskItemCommand { get; set; }

        private void AddNewTask()
        {
            {
                var newTaskItem = new TaskItem(TaskItemViewModelTitle, TaskItemViewModelDescription, TaskItemViewModelDueTime, TaskItemViewModelIsChecked);

                TaskItems.Add(newTaskItem);
            }
        }

        private void DeleteTaskItem()
        {
            var checkedTasks = TaskItems.Where(x => x.IsCompleted == true).ToList();

            foreach (var checkedTask in checkedTasks)
            {
                TaskItems.Remove(checkedTask);
            }
        }
    }
}
