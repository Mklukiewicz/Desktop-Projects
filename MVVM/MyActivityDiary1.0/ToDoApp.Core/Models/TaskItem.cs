using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _description = string.Empty;
        private DateTime? _finishDate;
        private bool _isTimerRunning;
        private TaskPriority _priority;
        private int _progressCurrentInt;
        private int _progressMaxInt;
        private string? _progressString;
        private TimeSpan _remainingTime;
        private bool _showTimer;
        private bool _showProgressFields = false;
        private DateTime _startDate;
        private bool _taskProgress;
        private string _title = string.Empty;


        public int DaysLeft => FinishDate.HasValue ? (FinishDate.Value - DateTime.Now).Days : 0;
        public string? Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public int Id { get; set; }                              
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public DateTime? FinishDate
        {
            get => _finishDate;
            set
            {
                if (_finishDate != value)
                {
                    _finishDate = value;
                    OnPropertyChanged(nameof(FinishDate));
                    OnPropertyChanged(nameof(TotalDays));
                    OnPropertyChanged(nameof(DaysLeft));
                    OnPropertyChanged(nameof(ProgressAsText));
                    OnPropertyChanged(nameof(ProgressAsDaysLeft));
                }
            }
        }
        public bool HasTaskStringProgress => !string.IsNullOrWhiteSpace(ProgressString);
        public bool IsMarked { get; set; }
        public bool IsFinished { get; set; }
        public bool IsTimerRunning
        {
            get => _isTimerRunning;
            set
            {
                _isTimerRunning = value;
                OnPropertyChanged(nameof(IsTimerRunning));
            }
        }
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
                }
            }
        }
        public string PriorityAsText => $"Priorytet: {PriorityDisplayNames[Priority]}";
        public static Dictionary<TaskPriority, string> PriorityDisplayNames => new()
        {
            { TaskPriority.Low, "Zerowy" },
            { TaskPriority.BelowNormal, "Niski" },
            { TaskPriority.Normal, "Średni" },
            { TaskPriority.AboveNormal, "Ważny" },
            { TaskPriority.High, "Bardzo ważny" }
        };
        public string ProgressAsDaysLeft => FinishDate.HasValue ? $"Zostało {DaysLeft} dni do końca" : string.Empty;
        public string ProgressAsText => $"{ProgressCurrentInt}/{ProgressMaxInt}";
        public int ProgressCurrentInt
        {
            get => _progressCurrentInt;
            set
            {
                _progressCurrentInt = value;
                OnPropertyChanged(nameof(ProgressCurrentInt));
                OnPropertyChanged(nameof(ProgressAsText));
            }
        }
        public int ProgressMaxInt
        {
            get => _progressMaxInt;
            set
            {
                _progressMaxInt = value;
                OnPropertyChanged(nameof(ProgressMaxInt));
                OnPropertyChanged(nameof(ProgressAsText));
            }
        }
        public string? ProgressString
        {
            get => _progressString;
            set
            {
                if (_progressString != value)
                {
                    _progressString = value;
                    OnPropertyChanged(nameof(ProgressString));
                    OnPropertyChanged(nameof(HasTaskStringProgress));
                }
            }
        }
        public TimeSpan RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged(nameof(RemainingTime));
            }
        }
        public bool ShowProgressFields
        {
            get => _showProgressFields;
            set
            {
                if (_showProgressFields != value)
                {
                    _showProgressFields = value;
                    OnPropertyChanged(nameof(ShowProgressFields));
                }
            }
        }
        public bool ShowTimer
        {
            get => _showTimer;
            set
            {
                _showTimer = value;
                OnPropertyChanged(nameof(ShowTimer));
            }
        }
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                    OnPropertyChanged(nameof(TotalDays));
                    OnPropertyChanged(nameof(DaysLeft));
                    OnPropertyChanged(nameof(ProgressAsText));
                    OnPropertyChanged(nameof(ProgressAsDaysLeft));
                }
            }
        }      
        public bool TaskProgress
        {
            get => _taskProgress;
            set
            {
                if (_taskProgress != value)
                {
                    _taskProgress = value;
                    OnPropertyChanged(nameof(TaskProgress));
                    ShowProgressFields = value; // <- dodaj to, jeśli jeszcze nie masz
                }
            }
        }
        public int? TotalDays => FinishDate.HasValue ? (FinishDate.Value - StartDate).Days : null;

        public TaskItem()
        {
            // Wymagany przez Entity Framework
        }

        public TaskItem(string title, string description, DateTime startTime, bool isMarkded, TaskPriority? taskPriority = null)
        {
            Title = title;
            Description = description;
            StartDate = startTime;
            IsMarked = isMarkded;
            Priority = taskPriority ?? TaskPriority.Low;
        }

        public TaskItem(string title, string description, DateTime startTime, DateTime finishDate, bool isMarkded,bool taskProgress,
            int progressMaxInt, int progressCurrentInt, string progressString, TaskPriority? taskPriority = null)
        {
            Title = title;
            Description = description;
            StartDate = startTime;
            FinishDate = finishDate;
            IsMarked = isMarkded;
            TaskProgress = taskProgress;
            ProgressMaxInt = progressMaxInt;
            ProgressCurrentInt = progressCurrentInt;
            ProgressString = progressString;
            Priority = taskPriority ?? TaskPriority.Low;
        }


        public event PropertyChangedEventHandler? PropertyChanged;     
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public enum TaskPriority
        {
            Low = 1,
            BelowNormal,
            Normal,
            AboveNormal,
            High
        }
    }
}
