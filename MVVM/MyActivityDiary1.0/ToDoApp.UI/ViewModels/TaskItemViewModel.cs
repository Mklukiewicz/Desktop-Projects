using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoApp.Core.Helpers;
using ToDoApp.Core.Models;
using ToDoApp.UI.Windows;
using System.Windows;
using System.ComponentModel;
using static ToDoApp.Core.Models.TaskItem;
using System.Windows.Threading;
using ToDoApp.DB.Repositories.Interfaces;
using ToDoApp.DB.Mappers;
using ToDoApp.DB.Models;

namespace ToDoApp.UI.ViewModels
{
    public class TaskItemViewModel : INotifyPropertyChanged
    {
        private int _currentProgress;
        private string? _dateValidationError;
        private DateTime? _finishDate;
        private bool _hasTaskProgress;
        private bool _hasTaskFinishDateProgress;
        private bool _hasTaskIntProgress;
        private bool _hasTaskStringProgress;
        private bool _isTimerRunning;
        private int _maxProgress;
        private string _motivationalQuote = "TEN TEKST BEDZIE PODMIENIANY MOTYWACYJNYMY HASŁAMI"; 
        private readonly IPointsHistoryRepository _pointsHistoryRepository;
        private TaskPriority _priority = TaskPriority.Low;
        private string? _progressValidationError;
        private TimeSpan _remainingTime;
        private readonly ITaskItemRepository _repository;
        private bool _showTimer;
        private DateTime? _startDate;
        private string? _stringProgressError;
        private string _taskStringProgress;
        private Dictionary<TaskItem, DispatcherTimer> _timers = new();
        private readonly Dictionary<TaskItem, DispatcherTimer> _saveTimers = new();
        private int _totalDaysLeft;
        

        public TaskItem? BuiltTask { get; set; }
        public bool CanSave => !HasDateError && !HasProgressError && !HasStringProgressError;
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
        public ObservableCollection<TaskItem> FinishedTaskItems { get; set; } = new ObservableCollection<TaskItem>();
        public ObservableCollection<DateGroup> GroupedFinishedTasks { get; } = new();
        public bool HasDateError => !string.IsNullOrEmpty(DateValidationError);
        public bool HasProgressError => !string.IsNullOrEmpty(ProgressValidationError);
        public bool HasStringProgressError => !string.IsNullOrEmpty(StringProgressError);
        public bool HasTaskIntProgress
        {
            get => _hasTaskIntProgress;
            set
            {
                _hasTaskIntProgress = value;
                OnPropertyChanged(nameof(HasTaskIntProgress));
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
        public bool IsTimerRunning
        {
            get => _isTimerRunning;
            set
            {
                if (_isTimerRunning != value)
                {
                    _isTimerRunning = value;
                    OnPropertyChanged(nameof(IsTimerRunning));
                }
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
        public string MotivationalQuote
        {
            get => _motivationalQuote;
            set
            {
                if (_motivationalQuote != value)
                {
                    _motivationalQuote = value;
                    OnPropertyChanged(nameof(MotivationalQuote));
                }
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
                    OnPropertyChanged(nameof(PriorityDisplayNames));
                }
            }
        }
        public string PriorityAsText => $"Priorytet: {PriorityDisplayNames[Priority]}";
        public Dictionary<TaskPriority, string> PriorityDisplayNames => new()
        {
            { TaskPriority.Low, "Zerowy" },
            { TaskPriority.BelowNormal, "Niski" },
            { TaskPriority.Normal, "Średni" },
            { TaskPriority.AboveNormal, "Ważny" },
            { TaskPriority.High, "Bardzo ważny" }
        };
        public Array PriorityValues => Enum.GetValues(typeof(TaskPriority));
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
        public TimeSpan RemainingTime
        {
            get => _remainingTime;
            set
            {
                if (_remainingTime != value)
                {
                    _remainingTime = value;
                    OnPropertyChanged(nameof(RemainingTime));
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
        public ObservableCollection<TaskItem> TaskItems { get; set; } = new ObservableCollection<TaskItem>();
        public string? TaskItemViewModelDescription { get; set; }
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
        public bool TaskItemViewModelIsChecked { get; set; }
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
        public string TaskItemViewModelTitle { get; set; }
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
        public int TotalDaysLeft
        {
            get => _totalDaysLeft;
            set
            {
                _totalDaysLeft = value;
                OnPropertyChanged(nameof(TotalDaysLeft));
            }
        }
        public IEnumerable<TaskItem> Top3ImportantOldestTasks =>
                                     TaskItems
                                    .Where(t => !t.IsFinished)
                                    .OrderByDescending(t => t.Priority)   
                                    .ThenBy(t => t.StartDate)             
                                    .Take(3);


        public ICommand DeleteTaskItemCommand { get; set; }
        public ICommand FinishTaskItemCommand { get; set; }
        public ICommand OpenAddTaskWindowCommand { get; set; }
        public ICommand OpenUpdateWindowCommand { get; set; }
        public ICommand StartCountdownCommand { get; set; }
        public ICommand StopCountDownCommand { get; set; }
        public ICommand StartTimerCommand { get; private set; }


        public TaskItemViewModel(ITaskItemRepository repository, IPointsHistoryRepository pointsHistoryRepository)
        {
            _repository = repository;
            _pointsHistoryRepository = pointsHistoryRepository;
            DeleteTaskItemCommand = new RelayCommand(async () => await DeleteTaskItem());
            OpenAddTaskWindowCommand = new RelayCommand(async () => await OpenAddTaskWindow());
            OpenUpdateWindowCommand = new RelayCommandParam(async p => await OpenUpdateWindow(p));
            FinishTaskItemCommand = new RelayCommandParam(async p => await FinishTaskItemAsync(p));
            StartCountdownCommand = new RelayCommandParam(async p => await StartCountdown(p));
            StopCountDownCommand = new RelayCommandParam(async p => await StopCountdown(p));
            StartTimerCommand = new RelayCommandParam(StartTimer);           
            _ = LoadTaskItemsAsync();
        }


        public void UpdateMotivationalQuote()
        {
            var quotes = new List<string>
        {
            "Nie zatrzymuj się!",
            "Działaj mimo wszystko!",
            "Dziś jest dobry dzień, by zacząć!",
            "Świat jest MÓJ więc niech daje mi to czego chcę",
            "Let's get it, champ!",
            "Każdy dzień to nowa szansa!",
            "Zrób dzisiaj to, czego inni nie chcą!",
            "Presja to PRZYWILEJ",
            "NO WORK NO CHECK",
            "Elevate over Bullshit",
            "Zaufaj Procesowi",
            "Kiedy ty nie trenujesz, ktoś inny staje się lepszy",
            "Bądz tak dobry żeby nie mogli Cię ignorować",
            "Hejterów nie mają tylko NoName",
            "Cotidie Morimur",
            "Angst fur Zukunft",
            "I am better than I thought I was",
            "Movin Forward",
            "Wcielam w życie mój plan doskonały!",
            "Becoming 1% better every day",
            "Dont stop grinding",
            "Bądź kim jesteś ale lepszym",
            "Long Live Ambition",
            "Maximum effort!",
        };

            var random = new Random();
            MotivationalQuote = quotes[random.Next(quotes.Count)];
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
        private async Task DeleteTaskItem()
        {
            var checkedTasks = TaskItems.Where(x => x.IsMarked).ToList();

            foreach (var task in checkedTasks)
            {
                var dbModel = task.ToDb();
                await _repository.DeleteAsync(dbModel.Id);

                TaskItems.Remove(task);
            }

            SortTasksByPriority();
            OnPropertyChanged(nameof(Top3ImportantOldestTasks));
        }
        private async Task FinishTaskItemAsync(object? parameter)
        {
            if (parameter is not TaskItem task)
                return;

            if (task.IsFinished && task.IsPointsGranted)
                return;

            task.IsFinished = true;
            task.FinishDate ??= DateTime.UtcNow;

            int pts = task.Priority switch
            {
                TaskPriority.Low => 1,
                TaskPriority.BelowNormal => 2,
                TaskPriority.Normal => 3,
                TaskPriority.AboveNormal => 4,
                TaskPriority.High => 5,
                _ => 0
            };

            if (!task.IsPointsGranted)
            {
                var historyEntry = new PointsHistoryDbModel
                {
                    TaskItemId = task.Id,
                    Date = DateTime.UtcNow,
                    Points = pts
                };

                await _pointsHistoryRepository.AddAsync(historyEntry);
                task.IsPointsGranted = true;
            }

            var dbModel = task.ToDb();
            if (dbModel.Id == 0)
                await _repository.AddAsync(dbModel);
            else
                await _repository.UpdateAsync(dbModel);

            task.Id = dbModel.Id;

            if (!FinishedTaskItems.Contains(task))
                FinishedTaskItems.Add(task);

            TaskItems.Remove(task);

            RefreshGroupedFinishedTasks();
            OnPropertyChanged(nameof(Top3ImportantOldestTasks));
        }
        private async Task LoadTaskItemsAsync()
        {
            var ongoingDbItems = await _repository.GetOngoingAsync(); 
            var finishedDbItems = await _repository.GetFinishedAsync(); 

            TaskItems.Clear();
            FinishedTaskItems.Clear();
            GroupedFinishedTasks.Clear();

            foreach (var db in ongoingDbItems)
            {
                var domain = TaskItemMapper.ToDomain(db);
                if (domain.RemainingTime > TimeSpan.Zero && !domain.IsFinished)
                {
                    domain.ShowTimer = true;
                }
                TaskItems.Add(domain);
            }

            foreach (var db in finishedDbItems)
            {
                var domain = TaskItemMapper.ToDomain(db);
                FinishedTaskItems.Add(domain);
            }

            var grouped = FinishedTaskItems
                .GroupBy(t => t.FinishDate.Value)
                .OrderByDescending(g => g.Key)
                .Select(g => new DateGroup
                {
                    Date = g.Key,
                    Tasks = new ObservableCollection<TaskItem>(g)
                });

            foreach (var group in grouped)
                GroupedFinishedTasks.Add(group);       

            SortTasksByPriority();
            RefreshGroupedFinishedTasks();
            OnPropertyChanged(nameof(Top3ImportantOldestTasks));
            OnPropertyChanged(nameof(GroupedFinishedTasks));
        }
        private async Task OpenAddTaskWindow()
        {
            var vm = new TaskItemViewModel(_repository, _pointsHistoryRepository);

            var addWindow = new AddTaskWindow
            {
                DataContext = vm
            };

            if (addWindow.ShowDialog() == true && vm.BuiltTask is not null)
            {
                var dbModel = TaskItemMapper.ToDb(vm.BuiltTask);

                await _repository.AddAsync(dbModel);

                vm.BuiltTask.Id = dbModel.Id;

                TaskItems.Add(vm.BuiltTask);

                SortTasksByPriority();
                OnPropertyChanged(nameof(Top3ImportantOldestTasks));
            }
        }
        private async Task StartCountdown(object parameter)
        {
            if (parameter is TaskItem task)
            {
                if (_timers.TryGetValue(task, out var existingTimer))
                {
                    existingTimer.Stop();
                    _timers.Remove(task);
                }

                if (_saveTimers.TryGetValue(task, out var existingSaveTimer))
                {
                    existingSaveTimer.Stop();
                    _saveTimers.Remove(task);
                }

                var timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };

                timer.Tick += (s, e) =>
                {
                    if (task.RemainingTime.TotalSeconds > 0)
                    {
                        task.RemainingTime = task.RemainingTime.Subtract(TimeSpan.FromSeconds(1));
                    }
                    else
                    {
                        timer.Stop();
                        task.IsTimerRunning = false;
                        _timers.Remove(task);

                        if (_saveTimers.TryGetValue(task, out var saveTimer))
                        {
                            saveTimer.Stop();
                            _saveTimers.Remove(task);
                        }

                        System.Media.SystemSounds.Exclamation.Play();
                        MessageBox.Show($"Zadanie \"{task.Title}\" zakończyło odliczanie!", "Czas minął!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                };

                var saveTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(10)
                };

                saveTimer.Tick += async (s, e) =>
                {
                    var dbItem = task.ToDb();
                    await _repository.UpdateAsync(dbItem);
                };

                _timers[task] = timer;
                _saveTimers[task] = saveTimer;

                task.IsTimerRunning = true;
                timer.Start();
                saveTimer.Start();
            }
        }
        private async void StartTimer(object parameter)
        {
            if (parameter is TaskItem task)
            {
                var window = new SetTimerWindow();
                if (window.ShowDialog() == true)
                {
                    task.RemainingTime = window.SelectedTime;
                    task.ShowTimer = true;

                    var dbModel = task.ToDb();
                    await _repository.UpdateAsync(dbModel);

                    if (window.StartImmediately)
                    {
                        StartCountdown(task);
                    }
                }
            }
        }
        private async Task StopCountdown(object parameter)
        {
            if (parameter is TaskItem task)
            {
                if (_timers.TryGetValue(task, out var timer))
                {
                    timer.Stop();
                    task.IsTimerRunning = false;
                    _timers.Remove(task);
                }

                if (_saveTimers.TryGetValue(task, out var saveTimer))
                {
                    saveTimer.Stop();
                    _saveTimers.Remove(task);
                }

                var dbItem = task.ToDb();
                _ = _repository.UpdateAsync(dbItem);
            }
            else
            {
                MessageBox.Show("Parameter to nie TaskItem.");
            }
        }
        private async Task OpenUpdateWindow(object? parameter)
        {
            if (parameter is not TaskItem taskItem)
                return;

            var vm = new TaskItemViewModel(_repository, _pointsHistoryRepository)
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

            var window = new UpdateTaskWindow { DataContext = vm };
            var result = window.ShowDialog();

            if (result == true && vm.BuiltTask is not null)
            {
                SortTasksByPriority();
                OnPropertyChanged(nameof(Top3ImportantOldestTasks));

                var dbModel = TaskItemMapper.ToDb(vm.BuiltTask);
                await _repository.UpdateAsync(dbModel);
            }
        }
        private void RefreshGroupedFinishedTasks()
        {
            GroupedFinishedTasks.Clear();

            var grouped = FinishedTaskItems
                .Where(t => t.FinishDate.HasValue)
                .GroupBy(t => t.FinishDate.Value.Date)
                .OrderByDescending(g => g.Key)
                .Select(g => new DateGroup
                {
                    Date = g.Key,
                    Tasks = new ObservableCollection<TaskItem>(g),
                    IsExpanded = false
                });

            foreach (var group in grouped)
                GroupedFinishedTasks.Add(group);
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


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;        
    }
}