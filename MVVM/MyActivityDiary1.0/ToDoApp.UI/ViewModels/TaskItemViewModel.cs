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
      
        public TaskItemViewModel()
        {
            AddNewTaskItem = new RelayCommand(AddNewTask);
        }
        public ObservableCollection<TaskItem> TaskItems { get; set; } = new ObservableCollection<TaskItem>();

        public  ICommand AddNewTaskItem { get; set; }
        
        private void AddNewTask()
        {
            {
                var newTaskItem = new TaskItem()
                {
                    Title = TaskItemViewModelTitle,
                    Description = TaskItemViewModelDescription,
                };

                TaskItems.Add(newTaskItem);
            }
        }
    }
}
