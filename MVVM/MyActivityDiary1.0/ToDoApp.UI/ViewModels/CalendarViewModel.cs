using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Core.Models;
using ToDoApp.Core.Helpers;

namespace ToDoApp.UI.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTime _currentDate = DateTime.Now;        
        private CalendarDay _selectedDay;

        public ObservableCollection<CalendarDay> CalendarDays { get; set; }
        public DateTime CurrentDate
        {
            get => _currentDate;
            set
            {
                if (_currentDate != value)
                {
                    _currentDate = value;
                    OnPropertyChanged(nameof(CurrentDate));
                    OnPropertyChanged(nameof(DisplayMonth));
                    GenerateCalendar(_currentDate);
                }
            }
        }
        public string CurrentMonthName => DateTime.Now.ToString("MMMM yyyy");
        public string DisplayMonth => $"{_currentDate.ToString("MMMM yyyy", new CultureInfo("pl-PL"))}";
        public CalendarDay SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                OnPropertyChanged(nameof(SelectedDay));
            }
        }
        public ICommand SelectDayCommand { get; }
        public ObservableCollection<TaskItem> TaskItems { get; set; }

        public ICommand GoNextCommand { get; set; }
        public ICommand GoPreviousCommand { get; set; }

        public CalendarViewModel(ObservableCollection<TaskItem> taskItems)
        {
            TaskItems = taskItems;
            CalendarDays = new ObservableCollection<CalendarDay>();
            TaskItems.CollectionChanged += (s, e) => GenerateCalendar(CurrentDate);
            GenerateCalendar(DateTime.Now);
            GoNextCommand = new RelayCommand(GoToNextMonth);
            GoPreviousCommand = new RelayCommand(GoToPreviousMonth);
            SelectDayCommand = new RelayCommandParam(ExecuteSelectDay);
        }

       
        public void GoToNextMonth() => CurrentDate = CurrentDate.AddMonths(1);
        public void GoToPreviousMonth() => CurrentDate = CurrentDate.AddMonths(-1);

        private void ExecuteSelectDay(object parameter)
        {
            if (parameter is CalendarDay day && !day.IsPlaceholder)
            {
                SelectedDay = day;
            }
        }
        private IEnumerable<TaskItem> GetTasksForDate(DateTime date)
        {
            return TaskItems.Where(t =>
                t.StartDate.Date == date.Date && !t.IsFinished);
        }
        private void GenerateCalendar(DateTime date)
        {
            CalendarDays.Clear();
            var firstDay = new DateTime(date.Year, date.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            int firstDayOfWeek = ((int)firstDay.DayOfWeek + 6) % 7;
            for (int i = 0; i < firstDayOfWeek; i++)
                CalendarDays.Add(new CalendarDay { IsPlaceholder = true });

            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayDate = new DateTime(date.Year, date.Month, day);
                var tasks = GetTasksForDate(dayDate);

                CalendarDays.Add(new CalendarDay
                {
                    DayNumber = day,
                    Date = dayDate,
                    Tasks = new ObservableCollection<TaskItem>(tasks)
                });
            }

            OnPropertyChanged(nameof(CalendarDays)); // to już niekonieczne, ale nie zaszkodzi
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
