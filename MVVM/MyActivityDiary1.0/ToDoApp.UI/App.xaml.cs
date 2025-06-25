using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DB;
using ToDoApp.UI.Windows;
using ToDoApp.UI.ViewModels;
using ToDoApp.DB.Repositories.Interfaces;
using ToDoApp.DB.Repositories; // jeśli używasz VM

namespace ToDoApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            // Dodajesz np. konfigurację DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            // Inne serwisy i ViewModel
           // services.AddSingleton<MainViewModel>();
            services.AddTransient<AddTaskWindow>();
            services.AddTransient<MainWindow>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();

            // Przypisz globalnie
            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
