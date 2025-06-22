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
        private ObservableCollection<TaskItem> _tasks = new();

        public int DayNumber { get; set; }
        public DateTime? Date { get; set; }
        public bool IsPlaceholder { get; set; } = false;
        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public IEnumerable<TaskItem> TasksSorted =>
                Tasks.OrderByDescending(t => t.Priority).Take(3);

        protected void OnPropertyChanged(string name)
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
