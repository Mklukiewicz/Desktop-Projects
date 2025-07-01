using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApp.DB.Repositories.Interfaces;
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly ITaskItemRepository _repository;
        private readonly IPointsHistoryRepository _pointsHistoryRepository;
        public TaskItemViewModel SharedViewModel { get; private set; }
        public PointsViewModel PointsViewModel { get; private set; }

        public MainPage(ITaskItemRepository repository, IPointsHistoryRepository pointsHistoryRepository)
        {
            InitializeComponent();

            _repository = repository;
            _pointsHistoryRepository = pointsHistoryRepository;
            SharedViewModel = new TaskItemViewModel(_repository, _pointsHistoryRepository);
            PointsViewModel =  new PointsViewModel(SharedViewModel.FinishedTaskItems, pointsHistoryRepository);
            DataContext = SharedViewModel;

            var homePage = new HomePage(SharedViewModel);
            var listPage = new ListOfTasksPage(SharedViewModel);
            var finishedTasksListPage = new FinishedTasksPage(SharedViewModel);
            var calendarPage = new CalendarPage { DataContext = new CalendarViewModel(SharedViewModel.TaskItems) };
            var pointsPage = new PointsPage(PointsViewModel)
            {
                DataContext = PointsViewModel
            };
           
            HomeTab.Content = homePage;
            ListTab.Content = listPage;
            FinishedTasksTab.Content = finishedTasksListPage;
            CalendarTab.Content = calendarPage;
            PointsTab.Content = pointsPage;

            Loaded += async (_, _) =>
            {
                await PointsViewModel.InitialiseAsync();
            };
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeTab.IsSelected)
            {
                if (DataContext is TaskItemViewModel vm)
                {
                    vm.UpdateMotivationalQuote();
                }
            }
        }
    }
}
