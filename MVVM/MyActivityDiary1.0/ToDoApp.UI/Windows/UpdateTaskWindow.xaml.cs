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
using ToDoApp.Core.Models;

namespace ToDoApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for UpdateTaskWindow.xaml
    /// </summary>
    public partial class UpdateTaskWindow : Window
    {
        public UpdateTaskWindow()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TaskItem task)
            {
                // 📝 Aktualizacja podstawowych pól
                task.Title = TitleTextBox.Text;
                task.Description = DescriptionTextBox.Text;
                task.StartDate = StartDatePicker.SelectedDate ?? DateTime.Now;

                // ✅ Sprawdź, czy użytkownik chce dodać postęp
                if (task.ShowProgressFields)
                {
                    task.TaskProgress = true;
                    task.FinishDate = FinishDatePicker.SelectedDate;

                    // 📊 Parsuj postęp liczbowy
                    int.TryParse(CurrentProgressTextBox.Text, out int currentProgress);
                    int.TryParse(MaxProgressTextBox.Text, out int maxProgress);
                    task.ProgressCurrentInt = currentProgress;
                    task.ProgressMaxInt = maxProgress;

                    // ✏️ Ustaw tekstowy postęp
                    task.ProgressString = ProgressStringTextBox.Text;
                }
                else
                {
                    // ❌ Zeruj dane postępu liczbowego
                    task.TaskProgress = false;
                    task.FinishDate = null;
                    task.ProgressCurrentInt = 0;
                    task.ProgressMaxInt = 0;

                    // ✅ Ustaw tekstowy postęp mimo braku liczbowego
                    task.ProgressString = ProgressStringTextBox.Text;
                }

                DialogResult = true;
                Close();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
