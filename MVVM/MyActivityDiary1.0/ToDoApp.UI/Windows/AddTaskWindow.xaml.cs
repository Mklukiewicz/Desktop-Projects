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
using System.Windows.Shapes;
using ToDoApp.UI.ViewModels;

namespace ToDoApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public string TaskTitle => TitleTextBox.Text;
        public string TaskDescription => DescriptionTextBox.Text;
        public DateTime StartDate => StartDatePicker.SelectedDate ?? DateTime.Now;
        public TaskItemViewModel ViewModel { get; }
        public AddTaskWindow()
        {
            InitializeComponent();
            ViewModel = new TaskItemViewModel();
            this.DataContext = ViewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.ViewModel;

            if (vm.HasTaskProgress == true)
            {
                var adjust = new AdjustTaskWindow()
                {
                    DataContext = vm // przekazujemy całą konfigurację
                };
                adjust.ShowDialog();
            }
            else
            {
                DialogResult = true;
                Close();
            }
             
        }
    }
}
