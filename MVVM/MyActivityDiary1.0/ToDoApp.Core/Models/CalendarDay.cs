using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.Models
{
    public class CalendarDay: INotifyPropertyChanged
    {
        public int DayNumber { get; set; }
        public bool IsPlaceholder { get; set; } = false;
        public DateTime? Date { get; set; }
        private ObservableCollection<TaskItem> _tasks = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        protected void OnPropertyChanged(string name)
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
