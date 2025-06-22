using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using static ToDoApp.Core.Models.TaskItem;

namespace ToDoApp.UI.ViewModels
{
    public class PointsViewModel : INotifyPropertyChanged// Zmienić nazewnictwo 
    {
        private readonly ObservableCollection<TaskItem> _finishedTasks;

        public PointsViewModel(ObservableCollection<TaskItem> finishedTasks)
        {
            _finishedTasks = finishedTasks;
            _finishedTasks.CollectionChanged += (s, e) => CalculatePoints();
            CalculatePoints();
        }

        public int ZerowyCount { get; private set; }
        public int NiskiCount { get; private set; }
        public int SredniCount { get; private set; }
        public int WaznyCount { get; private set; }
        public int BardzoWaznyCount { get; private set; }

        public int TotalPoints { get; private set; }
        public int CurrentLevel => TotalPoints / 5;
        public int PointsToNextLevel => 5 - (TotalPoints % 5);
        public int ProgressValue => TotalPoints;
        public int MaxProgress => (CurrentLevel + 1) * 5;

        private void CalculatePoints()
        {
            // Jeśli finishedTasks zawiera również nieukończone zadania — filtruj ręcznie
            var completedTasks = _finishedTasks.Where(t => t.IsFinished).ToList();

            ZerowyCount = completedTasks.Count(t => t.Priority == TaskPriority.Low);
            NiskiCount = completedTasks.Count(t => t.Priority == TaskPriority.BelowNormal);
            SredniCount = completedTasks.Count(t => t.Priority == TaskPriority.Normal);
            WaznyCount = completedTasks.Count(t => t.Priority == TaskPriority.AboveNormal);
            BardzoWaznyCount = completedTasks.Count(t => t.Priority == TaskPriority.High);

            TotalPoints = ZerowyCount * 1 +
                          NiskiCount * 2 +
                          SredniCount * 3 +
                          WaznyCount * 4 +
                          BardzoWaznyCount * 5;

            OnPropertyChanged(nameof(ZerowyCount));
            OnPropertyChanged(nameof(NiskiCount));
            OnPropertyChanged(nameof(SredniCount));
            OnPropertyChanged(nameof(WaznyCount));
            OnPropertyChanged(nameof(BardzoWaznyCount));
            OnPropertyChanged(nameof(TotalPoints));
            OnPropertyChanged(nameof(CurrentLevel));
            OnPropertyChanged(nameof(ProgressValue));
            OnPropertyChanged(nameof(MaxProgress));
            OnPropertyChanged(nameof(PointsToNextLevel));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
