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
    /// Interaction logic for SetTimerWindow.xaml
    /// </summary>
    public partial class SetTimerWindow : Window
    {
        public TimeSpan SelectedTime { get; private set; }

        public bool StartImmediately { get; private set; } = false;

        public SetTimerWindow()
        {
            InitializeComponent();
            HoursBox.ItemsSource = Enumerable.Range(0, 24);
            MinutesBox.ItemsSource = Enumerable.Range(0, 60);
            SecondsBox.ItemsSource = Enumerable.Range(0, 60);

            HoursBox.SelectedIndex = 0;
            MinutesBox.SelectedIndex = 0;
            SecondsBox.SelectedIndex = 0;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            SelectedTime = new TimeSpan(
                (int)HoursBox.SelectedItem,
                (int)MinutesBox.SelectedItem,
                (int)SecondsBox.SelectedItem);
            DialogResult = true;
            Close();
        }

        private void ConfirmOnly_Click(object sender, RoutedEventArgs e)
        {
            SelectedTime = new TimeSpan(
                (int)HoursBox.SelectedItem,
                (int)MinutesBox.SelectedItem,
                (int)SecondsBox.SelectedItem);
            StartImmediately = false;
            DialogResult = true;
            Close();
        }

        private void ConfirmAndStart_Click(object sender, RoutedEventArgs e)
        {
            SelectedTime = new TimeSpan(
                (int)HoursBox.SelectedItem,
                (int)MinutesBox.SelectedItem,
                (int)SecondsBox.SelectedItem);
            StartImmediately = true;
            DialogResult = true;
            Close();
        }
    }
}
