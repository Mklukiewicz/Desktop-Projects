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

namespace ToDoApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for TaskInfoWindow.xaml
    /// </summary>
    public partial class TaskInfoWindow : Window
    {
        //public string TaskTitle { get; set; } = TitleTextBox.Text;
        //public string TaskDescription => DescriptionTextBox.Text;
        //public string TaskDueDate => DateTimeTextBox.Text;
        //public string TaskProgress => ProgressTextBox.Text;
        public TaskInfoWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)// to bedzie do usuniecia 
        {
            Close();
        }
    }
}
