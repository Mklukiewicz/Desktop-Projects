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
using System.Reflection.Metadata;

namespace ToDoApp.UI.ViewModels
{
    public class TaskItemViewModel
    {
        public string TaskItemViewModelTitle { get; set; }// nie ma referencji usunac 
        public string? TaskItemViewModelDescription { get; set; }
        public DateTime TaskItemViewModelDueTime { get; set; }
        public bool TaskItemViewModelIsChecked { get; set; }
        public bool? HasTaskProgress { get; set; }
        public bool? HasTaskFinishDateProgress { get; set; }
        public bool? HasTaskIntProgress { get; set; }
        public bool? HasTaskStringProgress { get; set; }

        public TaskItemViewModel()
        {
            DeleteTaskItemCommand = new RelayCommand(DeleteTaskItem);
            OpenAddTaskWindowCommand = new RelayCommand(OpenAddTaskWindow);
            OpenInfoWindowCommand = new RelayCommandGeneric<TaskItem>(OpenInfoWindow);
            OpenUpdateWindowCommand = new RelayCommandParam(OpenUpdateWindow);
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
               var newTask = new TaskItem(window.TaskTitle, window.TaskDescription, window.StartDate, false);

                var vm = window.DataContext as TaskItemViewModel;

                if (vm != null && vm.HasTaskProgress == true)
                {
                    var adjustWindow = new AdjustTaskWindow
                    {
                        DataContext = vm
                    };
                    adjustWindow.ShowDialog();
                }
                else
                {
                    TaskItems.Add(newTask);
                }

            }                     
        }

        private void OpenUpdateWindow( object? parameter)
        {
            if (parameter is Tuple<object, object> tuple &&
                 tuple.Item1 is TaskItem taskItem &&
                 tuple.Item2 is Window infoWindow)
            { 
                infoWindow.Close();

                var window = new UpdateTaskWindow
                {
                    DataContext = taskItem
                };

                window.TitleTextBox.Text = taskItem.Title;
                window.DescriptionTextBox.Text = taskItem.Description;
                window.DateTextBox.Text = taskItem.DueDate.ToString();

                window.ShowDialog();
            }
        }

        private void OpenInfoWindow(TaskItem taskItem)// w zadaniu mozna dodać również regress
        {
            var window = new TaskInfoWindow()
            {
                DataContext = taskItem,
                ParentViewModel = this
            };

            window.ShowDialog();
        }          
    }
}
