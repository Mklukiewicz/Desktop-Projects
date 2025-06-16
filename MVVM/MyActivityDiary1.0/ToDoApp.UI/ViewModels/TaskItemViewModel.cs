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
using System.ComponentModel;

namespace ToDoApp.UI.ViewModels
{
    public class TaskItemViewModel : INotifyPropertyChanged
    {
        private DateTime? _startDate;
        private DateTime? _finishDate;
        private int _totalDaysLeft;

        public string TaskItemViewModelTitle { get; set; }
        public string? TaskItemViewModelDescription { get; set; }
        public DateTime? TaskItemViewModelStartTime { get; set; }
        public bool TaskItemViewModelIsChecked { get; set; }
        public bool? HasTaskProgress { get; set; }
        private bool _hasTaskFinishDateProgress;
        public bool HasTaskFinishDateProgress
        {
            get => _hasTaskFinishDateProgress;
            set
            {
                _hasTaskFinishDateProgress = value;
                OnPropertyChanged(nameof(HasTaskFinishDateProgress));
                CalculateTotalDays();
            }
        }

        public bool? HasTaskIntProgress { get; set; }
        public bool? HasTaskStringProgress { get; set; }
        public DateTime? StartDate
        {
            get => _startDate ?? DateTime.Today;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
        }

        public DateTime? FinishDate
        {
            get => _finishDate;
            set
            {
                _finishDate = value;
                OnPropertyChanged(nameof(FinishDate));
                CalculateTotalDays();
            }
        }

        public int TotalDaysLeft
        {
            get => _totalDaysLeft;
            set
            {
                _totalDaysLeft = value;
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
        }

        private string? _dateValidationError;
        public string? DateValidationError
        {
            get => _dateValidationError;
            set
            {
                _dateValidationError = value;
                OnPropertyChanged(nameof(DateValidationError));
                OnPropertyChanged(nameof(HasDateError));
            }
        }

        public bool HasDateError => !string.IsNullOrEmpty(DateValidationError);

        

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

        private void OpenAddTaskWindow()
        {
            var newTaskVm = new TaskItemViewModel();

            var window = new AddTaskWindow()
            {
                DataContext = newTaskVm
            };
                    
            if (window.ShowDialog() == true)
            {
               var newTask = new TaskItem(newTaskVm.TaskItemViewModelTitle, newTaskVm.TaskItemViewModelDescription, newTaskVm.StartDate.Value, false);

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
                window.DateTextBox.Text = taskItem.StartDate.ToString();

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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void CalculateTotalDays()
        {
            if (StartDate.HasValue && FinishDate.HasValue)
            {
                if (FinishDate.Value < StartDate.Value)
                {
                    DateValidationError = "Data zakończenia nie może być wcześniejsza niż data rozpoczęcia.";
                    _totalDaysLeft = 0;
                    OnPropertyChanged(nameof(TotalDaysLeft));
                    return;
                }

                DateValidationError = null;
                _totalDaysLeft = (FinishDate.Value - StartDate.Value).Days;
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
            else
            {
                DateValidationError = null;
                _totalDaysLeft = 0;
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
        }

    }
}
