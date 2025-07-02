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
using ToDoApp.DB.Repositories;

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
            base.OnStartup(e);
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddTransient<AddTaskWindow>();
            services.AddTransient<MainWindow>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
