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

namespace ToDoApp.UI.Controls
{
    /// <summary>
    /// Interaction logic for WorkTaskControl.xaml
    /// </summary>
    public partial class WorkTaskControl : UserControl// tutaj poprawić paddingi i marginesyw xamlu
    {
        public WorkTaskControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
