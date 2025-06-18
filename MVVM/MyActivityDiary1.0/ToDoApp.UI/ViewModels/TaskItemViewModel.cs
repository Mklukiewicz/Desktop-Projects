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
using static ToDoApp.Core.Models.TaskItem;

namespace ToDoApp.UI.ViewModels
{
    public class TaskItemViewModel : INotifyPropertyChanged
    {
        private DateTime? _startDate;
        private DateTime? _finishDate;
        private int _totalDaysLeft;
        private int _currentProgress;
        private int _maxProgress;
        private string? _progressValidationError;
        private bool _hasTaskIntProgress;
        private bool _hasTaskFinishDateProgress;
        private bool _hasTaskStringProgress;
        private string _taskStringProgress;
        private string? _stringProgressError;
        private TaskPriority _priority = TaskPriority.Low;
        private bool _hasTaskProgress;

        public string TaskItemViewModelTitle { get; set; }
        public string? TaskItemViewModelDescription { get; set; }
        public bool TaskItemViewModelIsChecked { get; set; }
        public bool CanSave => !HasDateError && !HasProgressError && !HasStringProgressError;       
        public bool HasTaskProgress
        {
            get => _hasTaskProgress;
            set
            {
                if (_hasTaskProgress != value)
                {
                    _hasTaskProgress = value;
                    OnPropertyChanged(nameof(HasTaskProgress));
                }
            }
        }
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
        public bool HasTaskIntProgress
        {
            get => _hasTaskIntProgress;
            set
            {
                _hasTaskIntProgress = value;
                OnPropertyChanged(nameof(HasTaskIntProgress));
            }
        }
        public bool HasTaskStringProgress
        {
            get => _hasTaskStringProgress;
            set
            {
                _hasTaskStringProgress = value;
                OnPropertyChanged(nameof(HasTaskStringProgress));
                ValidateStringProgress();
            }
        }
        public DateTime? TaskItemViewModelStartDate
        {
            get => _startDate ?? DateTime.Today;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(TaskItemViewModelStartDate));
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
        }

        public DateTime? TaskItemViewModelFinishDate
        {
            get => _finishDate;
            set
            {
                _finishDate = value;
                OnPropertyChanged(nameof(TaskItemViewModelFinishDate));
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
                OnPropertyChanged(nameof(CanSave));
            }
        }       
        public int CurrentProgress
        {
            get => _currentProgress;
            set
            {
                _currentProgress = value;
                OnPropertyChanged(nameof(CurrentProgress));
                ValidateProgress();
            }
        }
        public int MaxProgress
        {
            get => _maxProgress;
            set
            {
                _maxProgress = value;
                OnPropertyChanged(nameof(MaxProgress));
                ValidateProgress();
            }
        }
        public string? ProgressValidationError
        {
            get => _progressValidationError;
            set
            {
                _progressValidationError = value;
                OnPropertyChanged(nameof(ProgressValidationError));
                OnPropertyChanged(nameof(HasProgressError));
                OnPropertyChanged(nameof(CanSave));
            }
        }
        public string TaskStringProgress
        {
            get => _taskStringProgress;
            set
            {
                _taskStringProgress = value;
                OnPropertyChanged(nameof(TaskStringProgress));
                ValidateStringProgress();
                
            }
        }        
        public string? StringProgressError
        {
            get => _stringProgressError;
            set
            {
                _stringProgressError = value;
                OnPropertyChanged(nameof(StringProgressError));
                OnPropertyChanged(nameof(HasStringProgressError));
                OnPropertyChanged(nameof(CanSave));
            }
        }
        public bool HasStringProgressError => !string.IsNullOrEmpty(StringProgressError);
        public bool HasProgressError => !string.IsNullOrEmpty(ProgressValidationError);
        public bool HasDateError => !string.IsNullOrEmpty(DateValidationError);
        public Action? OpenAdjustWindowCallback { get; set; }
        public TaskItem? BuiltTask { get; set; }
        public TaskPriority Priority
        {
            get => _priority;
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged(nameof(Priority));
                    OnPropertyChanged(nameof(PriorityAsText));
                    OnPropertyChanged(nameof(PriorityDisplayNames));
                }
            }
        }
        public Array PriorityValues => Enum.GetValues(typeof(TaskPriority));

        public Dictionary<TaskPriority, string> PriorityDisplayNames => new()
        {
            { TaskPriority.Low, "Zerowy" },
            { TaskPriority.BelowNormal, "Niski" },
            { TaskPriority.Normal, "Średni" },
            { TaskPriority.AboveNormal, "Ważny" },
            { TaskPriority.High, "Bardzo ważny" }
        };

        public string PriorityAsText => $"Priorytet: {PriorityDisplayNames[Priority]}";


        public TaskItemViewModel()
        {
            DeleteTaskItemCommand = new RelayCommand(DeleteTaskItem);
            OpenAddTaskWindowCommand = new RelayCommand(OpenAddTaskWindow);
            OpenUpdateWindowCommand = new RelayCommandParam(OpenUpdateWindow);
            FinishTaskItemCommand = new RelayCommandParam(FinishTaskItem);
        }
        public ObservableCollection<TaskItem> TaskItems { get; set; } = new ObservableCollection<TaskItem>();
        public ObservableCollection<TaskItem> FinishedTaskItems { get; set; } = new ObservableCollection<TaskItem>();

        public  ICommand DeleteTaskItemCommand { get; set; }
        public  ICommand OpenAddTaskWindowCommand { get; set; }
        public  ICommand OpenUpdateWindowCommand { get; set; }
        public  ICommand FinishTaskItemCommand { get; set; }

       
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
            var vm = new TaskItemViewModel();

            var addWindow = new AddTaskWindow
            {
                DataContext = vm
            };

            if (addWindow.ShowDialog() == true && vm.BuiltTask != null)
            {
                TaskItems.Add(vm.BuiltTask);
                SortTasksByPriority();
            }
        }

        private void OpenUpdateWindow(object? parameter)// w zadaniu mozna dodać również regress
        {
            if (parameter is TaskItem taskItem)
            {
                

                var vm = new TaskItemViewModel
                {
                    TaskItemViewModelTitle = taskItem.Title,
                    TaskItemViewModelDescription = taskItem.Description,
                    TaskItemViewModelStartDate = taskItem.StartDate,
                    TaskItemViewModelFinishDate = taskItem.FinishDate,
                    HasTaskProgress = taskItem.TaskProgress,
                    MaxProgress = taskItem.ProgressMaxInt,
                    CurrentProgress = taskItem.ProgressCurrentInt,
                    TaskStringProgress = taskItem.ProgressString,
                    Priority = (TaskPriority)taskItem.Priority,
                    BuiltTask = taskItem
                };
                var window = new UpdateTaskWindow()
                {
                    DataContext = vm
                };

                var result = window.ShowDialog();
                if (result == true)
                {
                    SortTasksByPriority();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void CalculateTotalDays()
        {
            if (TaskItemViewModelStartDate.HasValue && TaskItemViewModelFinishDate.HasValue)
            {
                if (TaskItemViewModelFinishDate.Value < TaskItemViewModelStartDate.Value)
                {
                    DateValidationError = "Data zakończenia nie może być wcześniejsza niż data rozpoczęcia.";
                    _totalDaysLeft = 0;
                    OnPropertyChanged(nameof(TotalDaysLeft));
                    return;
                }

                DateValidationError = null;
                _totalDaysLeft = (TaskItemViewModelFinishDate.Value - TaskItemViewModelStartDate.Value).Days;
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
            else
            {
                DateValidationError = null;
                _totalDaysLeft = 0;
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
        }

        private void ValidateProgress()
        {
            if (MaxProgress < CurrentProgress)
            {
                ProgressValidationError = "Maksymalny postęp nie może być mniejszy niż aktualny.";
            }
            else
            {
                ProgressValidationError = null;
            }
        }
        private void ValidateStringProgress()
        {
            if (HasTaskStringProgress && string.IsNullOrWhiteSpace(TaskStringProgress))
            {
                StringProgressError = "Postęp opisowy nie może być pusty.";
            }
            else
            {
                StringProgressError = null;
            }
        }       
        private void FinishTaskItem(object? parameter)
        {
            if (parameter is TaskItem taskItemToFinish)
            {
                taskItemToFinish.IsFinished = true;

                if (!FinishedTaskItems.Contains(taskItemToFinish))
                    FinishedTaskItems.Add(taskItemToFinish);

                TaskItems.Remove(taskItemToFinish);
                SortTasksByPriority();
            }          
        }
        private void SortTasksByPriority()
        {
            var sorted = TaskItems.OrderByDescending(t => t.Priority).ToList();

            TaskItems.Clear();
            foreach (var task in sorted)
            {
                TaskItems.Add(task);
            }
        }
    }
}