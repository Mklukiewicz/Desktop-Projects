//using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using ToDoApp.Core.Helpers;
using ToDoApp.Core.Models;
using ToDoApp.UI.Windows;
using System.Linq;
using System.Windows;

namespace ToDoApp.UI.ViewModels
{
    public class TaskItemViewModel
    {
        public string TaskItemViewModelTitle { get; set; }// nie ma referencji usunac 
        public string? TaskItemViewModelDescription { get; set; }
        public DateTime TaskItemViewModelDueTime { get; set; }
        public bool TaskItemViewModelIsChecked { get; set; }
      
        public TaskItemViewModel()
        {
            DeleteTaskItemCommand = new RelayCommand(DeleteTaskItem);
            OpenAddTaskWindowCommand = new RelayCommand(OpenAddTaskWindow);
            OpenInfoWindowCommand = new RelayCommandGeneric<TaskItem>(OpenInfoWindow);
            OpenUpdateWindowCommand = new RelayCommandGeneric<TaskItem>(OpenUpdateWindow);
        }
        public ObservableCollection<TaskItem> TaskItems { get; set; } = new ObservableCollection<TaskItem>();

        public  ICommand AddNewTaskItemCommand { get; set; }
        public  ICommand DeleteTaskItemCommand { get; set; }
        public  ICommand OpenAddTaskWindowCommand { get; set; }
        public  ICommand OpenInfoWindowCommand { get; set; }
        public  ICommand OpenUpdateWindowCommand { get; set; }
       
        private void DeleteTaskItem()
        {
            var checkedTasks = TaskItems.Where(x => x.IsCompleted == true).ToList();

            foreach (var checkedTask in checkedTasks)
            {
                TaskItems.Remove(checkedTask);
            }
        }

        private void OpenAddTaskWindow()// rozpisać tą metode co i jak
        {
            var window = new AddTaskWindow();
            if (window.ShowDialog() == true)
            {
                var newTask = new TaskItem(window.TaskTitle, window.TaskDescription, window.DueDate, false);
                TaskItems.Add(newTask); // lub taskService.Add(newTask)
            }
        }

        private void OpenUpdateWindow(TaskItem taskItem)
        {
            var window = new UpdateTaskWindow()
            {
                DataContext = taskItem,
            };

            window.TitleTextBox.Text = taskItem.Title;
            window.DescriptionTextBox.Text = taskItem.Description;
            window.DateTextBox.Text = taskItem.DueDate.ToString();
            window.ProgressTextBox.Text = taskItem.TaskProgres.ToString();


           
            window.ShowDialog();
        }

        private void OpenInfoWindow(TaskItem taskItem)
        {
            var window = new TaskInfoWindow()
            {
                DataContext = this,
                CurrentTaskItem = taskItem,
                
            };

            window.TitleTextBox.Text = taskItem.Title;
            window.DescriptionTextBox.Text = taskItem.Description;
            window.DateTimeTextBox.Text = taskItem.DueDate.ToString();
            window.ProgressTextBox.Text = taskItem.TaskProgres.ToString();

            window.ShowDialog();
        }          
    }
}
