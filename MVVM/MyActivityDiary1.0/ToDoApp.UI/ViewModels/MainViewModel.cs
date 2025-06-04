using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Core.Models;
using ToDoApp.Core.Services.Interfaces;

namespace ToDoApp.UI.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ITaskService taskService_;

        public ObservableCollection<TaskItem> TaskItemsList =  new ();

        public string NewTaskTitle { get; set; }
        public string NewTaskDescription { get; set; }
        public DateTime NewTaskDueTime { get; set; }
        public bool SelectedTask {  get; set; }

        public MainViewModel(ITaskService taskService)
        {
            taskService_ = taskService;
        }
        public void LoadTasks()// tutaj jeszcze poprawić zwracanie
        {
           var list = taskService_.GetAll();
           foreach (var item in list)
            {
                TaskItemsList.Add(item);
            }

            //PropertyChanged(TaskItemsList);
        }

        //public ICommand AddTask()
        //{
        //    var newTask = new TaskItem(NewTaskTitle, NewTaskDescription, NewTaskDueTime, SelectedTask);
        //    taskService_.Add(newTask);
        //    return 
        //}

        //public ICommand RemoveTask(int id)// tutaj musi byc jeszcze zapisanie w bazie danych
        //{
        //    var taskToDelete = taskService_.GetById(id);
        //    if(taskToDelete != null && taskToDelete.CheckMarked == true)
        //    {
        //        taskService_.Delete();
        //    }
        //}

        //public ICommand UpdateTask(int id)// tutaj musi byc jeszcze zapisanie w bazie danych 
        //{
        //    var taskToUpdate = taskService_.GetById(id);
        //    taskToUpdate.Title = NewTaskTitle;
        //    taskToUpdate.Description = NewTaskDescription;
        //    taskToUpdate.DueDate = NewTaskDueTime;
        //    taskToUpdate.CheckMarked = false;
        //}

        //public ICommand CheckMarkTask(int id)// tutaj musi byc jeszcze zapisanie w bazie danych
        //{
        //    var taskToMarked = taskService_.GetById(id);
        //    taskService_.CheckMarkTask(taskToMarked);
        //}
    }
}
