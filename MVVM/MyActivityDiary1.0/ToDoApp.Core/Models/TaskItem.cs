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
        public int Id { get; set; }// to bedzie wykorzystane do bazy danych 

        private string _title = string.Empty;// priorytetowośc zadania

        private string _description = string.Empty;

        private DateTime _startDate;

        private DateTime? _finishDate;
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

        public bool IsCompleted { get; set; }
        public bool TaskProgress { get; set; }
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
        public int CurrentProgressDay { get; set; }
        public int? TotalDays => FinishDate.HasValue ? (FinishDate.Value - StartDate).Days : null;

        public int DaysLeft => FinishDate.HasValue ? (FinishDate.Value - DateTime.Now).Days : 0;

        public string ProgressAsText => FinishDate.HasValue ? $"{(TotalDays - DaysLeft)}/{TotalDays}" : string.Empty;

        public string ProgressAsDaysLeft => FinishDate.HasValue ? $"Zostało {DaysLeft} dni do końca" : string.Empty;
        public int ProgressMaxInt { get; set; }
        public int ProgressCurrentInt { get; set; }// trzeba pamietac że current progress musi byc zawsze mniejszy niż max progress
        public string? ProgressString { get; set; }
     
        public TaskItem(string title, string description, DateTime dueTime, bool isMarkded)
        {
            Title = title;
            Description = description;
            StartDate = dueTime;
            IsCompleted = isMarkded;
        }

        public bool IsOverdue => !IsCompleted && StartDate.Date < DateTime.Now.Date;//metoda sprawdzajaca czy zadanie jest zaległe, niepotrzebne usunać

        public event PropertyChangedEventHandler? PropertyChanged;
      
        protected void OnPropertyChanged(string propertyName)// metoda pomocniczna wywołująca event change 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
