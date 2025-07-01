using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApp.DB.Repositories;
using ToDoApp.DB;
using ToDoApp.UI.Pages;
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dbContext = new AppDbContextFactory().CreateDbContext(Array.Empty<string>());
            var repository = new TaskItemRepository(dbContext);
            var pointsRepository = new PointsHistoryRepository(dbContext);
            var mainPage = new MainPage(repository, pointsRepository);

            MainFrame.Navigate(mainPage);
        }

    }
}