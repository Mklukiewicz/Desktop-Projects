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

        private string _title = string.Empty;

        private string _description = string.Empty;
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
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool TaskProgress { get; set; }
        public DateTime FinishDate { get; set; }// tutaj trzeba dodać różnice dni 
        public int CurrentProgressDay { get; set; }
        public int TotalDays { get; set; }
        public int ProgressMaxInt { get; set; }
        public int ProgressCurrentInt { get; set; }// trzeba pamietac że current progress musi byc zawsze mniejszy niż max progress
        public string? ProgressString { get; set; }
     
        public TaskItem(string title, string description, DateTime dueTime, bool isMarkded)
        {
            Title = title;
            Description = description;
            DueDate = dueTime;
            IsCompleted = isMarkded;
        }

        public bool IsOverdue => !IsCompleted && DueDate.Date < DateTime.Now.Date;//metoda sprawdzajaca czy zadanie jest zaległe

        public event PropertyChangedEventHandler? PropertyChanged;

        //public int SetProgres(int progres)// nie ma referencji usunac , metoda pomocnicza zobaczymy czy bedzie uzywana
        //{
        //   return this.TaskProgres = progres;
        //}

        //public int AddProgress()// nie ma referencji usunac , metoda pomocnicza zobaczymy czy bedzie uzywana
        //{
        //    return TaskProgres++;
        //}

        //public int MinusProgress()// nie ma referencji usunac , metoda pomocnicza zobaczymy czy bedzie uzywana
        //{
        //    return TaskProgres--;
        //}

        protected void OnPropertyChanged(string propertyName)// metoda pomocniczna wywołująca event change 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
