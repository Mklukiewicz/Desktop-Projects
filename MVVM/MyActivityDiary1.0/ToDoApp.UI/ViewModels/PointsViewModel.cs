using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using ToDoApp.DB.Mappers;
using ToDoApp.DB.Repositories.Interfaces;
using static ToDoApp.Core.Models.TaskItem;

namespace ToDoApp.UI.ViewModels;

/// <summary>
///  Liczy punkty i zapisuje historię do bazy.
/// </summary>
public class PointsViewModel : INotifyPropertyChanged
{
    private readonly ObservableCollection<TaskItem> _finishedTasks;
    private readonly IPointsHistoryRepository _pointsRepo;

    public PointsViewModel(ObservableCollection<TaskItem> finishedTasks,
                           IPointsHistoryRepository pointsRepo)
    {
        _finishedTasks = finishedTasks;
        _pointsRepo = pointsRepo;

        _finishedTasks.CollectionChanged += FinishedTasks_CollectionChanged;
        _ = InitialiseAsync(); // domyślne wyliczenie na starcie
    }

    #region Właściwości

    public int ZerowyCount { get; private set; }
    public int NiskiCount { get; private set; }
    public int SredniCount { get; private set; }
    public int WaznyCount { get; private set; }
    public int BardzoWaznyCount { get; private set; }

    public int TotalPoints { get; private set; }
    public int CurrentLevel => TotalPoints / 5;
    public int PointsToNext => 5 - (TotalPoints % 5);
    public int ProgressValue => TotalPoints;
    public int MaxProgress => (CurrentLevel + 1) * 5;

    #endregion

    #region Re-liczenie punktów

    private void Recalculate(IEnumerable<TaskItem> source)
    {
        var completed = source.Where(t => t.IsFinished).ToList();

        ZerowyCount = completed.Count(t => t.Priority == TaskPriority.Low);
        NiskiCount = completed.Count(t => t.Priority == TaskPriority.BelowNormal);
        SredniCount = completed.Count(t => t.Priority == TaskPriority.Normal);
        WaznyCount = completed.Count(t => t.Priority == TaskPriority.AboveNormal);
        BardzoWaznyCount = completed.Count(t => t.Priority == TaskPriority.High);

        TotalPoints =
            ZerowyCount * 1 +
            NiskiCount * 2 +
            SredniCount * 3 +
            WaznyCount * 4 +
            BardzoWaznyCount * 5;

        NotifyPointsChanged();
    }

    private void NotifyPointsChanged()
    {
        OnPropertyChanged(nameof(ZerowyCount));
        OnPropertyChanged(nameof(NiskiCount));
        OnPropertyChanged(nameof(SredniCount));
        OnPropertyChanged(nameof(WaznyCount));
        OnPropertyChanged(nameof(BardzoWaznyCount));
        OnPropertyChanged(nameof(TotalPoints));
        OnPropertyChanged(nameof(CurrentLevel));
        OnPropertyChanged(nameof(PointsToNext));
        OnPropertyChanged(nameof(ProgressValue));
        OnPropertyChanged(nameof(MaxProgress));
    }

    #endregion

    #region Zdarzenia kolekcji

    private async void FinishedTasks_CollectionChanged(object? sender,
                                                       NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (TaskItem t in e.NewItems!)
            {
                if (t.IsFinished)
                    await SavePointsForTaskAsync(t);
            }
        }

        Recalculate(_finishedTasks);
    }

    #endregion

    #region Zapis historii punktów

    private async Task SavePointsForTaskAsync(TaskItem task)
    {
        int pts = task.Priority switch
        {
            TaskPriority.Low => 1,
            TaskPriority.BelowNormal => 2,
            TaskPriority.Normal => 3,
            TaskPriority.AboveNormal => 4,
            TaskPriority.High => 5,
            _ => 0
        };

        var domain = new PointsHistory
        {
            Date = DateTime.UtcNow,
            Points = pts,
            TaskItemId = task.Id
        };

        var dbModel = PointsHistoryMapper.ToDb(domain);
        await _pointsRepo.AddAsync(dbModel);
    }

    #endregion

    #region Inicjalizacja z bazy

    public async Task InitialiseAsync()
    {
        var history = await _pointsRepo.GetAllPointsHistoryAsync();

        ZerowyCount = history.Count(h => h.Points == 1);
        NiskiCount = history.Count(h => h.Points == 2);
        SredniCount = history.Count(h => h.Points == 3);
        WaznyCount = history.Count(h => h.Points == 4);
        BardzoWaznyCount = history.Count(h => h.Points == 5);
        TotalPoints = history.Sum(h => h.Points);

        NotifyPointsChanged();
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    #endregion
}
