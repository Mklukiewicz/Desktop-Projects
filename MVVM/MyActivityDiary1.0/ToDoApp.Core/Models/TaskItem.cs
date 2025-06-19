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
        private bool _showProgressFields = false;// ulozyc fieldy w kolejnosci alfabetycznej przy robieniu CleanCode
        private int _progressMaxInt;// dodać graf tygodniowy który opisuje postęp zadan
        private bool _taskProgress;
        private int _progressCurrentInt;
        private string? _progressString;
        private string _description = string.Empty;
        private DateTime _startDate;
        private DateTime? _finishDate;
        private string _title = string.Empty;
        private TaskPriority _priority;

        public int Id { get; set; }// to bedzie wykorzystane do bazy danych                                     
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
        }// ulozyc property w kolejnosci alfabetycznej przy robieniu CleanCode
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
        public bool IsCompleted { get; set; }// zmienic nazwe z IsCompleted na np. IsMarked
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
        public int? TotalDays => FinishDate.HasValue ? (FinishDate.Value - StartDate).Days : null;
        public int DaysLeft => FinishDate.HasValue ? (FinishDate.Value - DateTime.Now).Days : 0;
        public string ProgressAsText => $"{ProgressCurrentInt}/{ProgressMaxInt}";
        public string ProgressAsDaysLeft => FinishDate.HasValue ? $"Zostało {DaysLeft} dni do końca" : string.Empty;
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
        public bool HasTaskStringProgress => !string.IsNullOrWhiteSpace(ProgressString);
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
        public bool IsFinished { get; set; }
        public enum TaskPriority
        {
            Low = 1,        
            BelowNormal,    
            Normal,         
            AboveNormal,    
            High            
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

        public static Dictionary<TaskPriority, string> PriorityDisplayNames => new()
        {
            { TaskPriority.Low, "Zerowy" },
            { TaskPriority.BelowNormal, "Niski" },
            { TaskPriority.Normal, "Średni" },
            { TaskPriority.AboveNormal, "Ważny" },
            { TaskPriority.High, "Bardzo ważny" }
        };

        public string PriorityAsText => $"Priorytet: {PriorityDisplayNames[Priority]}";

        public TaskItem(string title, string description, DateTime startTime, bool isMarkded, TaskPriority? taskPriority = null)
        {
            Title = title;
            Description = description;
            StartDate = startTime;
            IsCompleted = isMarkded;
            Priority = taskPriority ?? TaskPriority.Low;
        }

        public TaskItem(string title, string description, DateTime startTime, DateTime finishDate, bool isMarkded,bool taskProgress,
            int progressMaxInt, int progressCurrentInt, string progressString, TaskPriority? taskPriority = null)
        {
            Title = title;
            Description = description;
            StartDate = startTime;
            FinishDate = finishDate;
            IsCompleted = isMarkded;
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

    }
}
