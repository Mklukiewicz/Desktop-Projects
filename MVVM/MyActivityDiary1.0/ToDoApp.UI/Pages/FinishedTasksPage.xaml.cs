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
using ToDoApp.Core.Models;
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for FinishedTasksPage.xaml
    /// </summary>
    public partial class FinishedTasksPage : UserControl
    {
        public FinishedTasksPage(TaskItemViewModel sharedViewModel)
        {
            InitializeComponent();

            DataContext = sharedViewModel;
        }

        private void ToggleGroupVisibility(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is DateGroup group)
            {
                group.IsExpanded = !group.IsExpanded;
            }
        }
    }
}
