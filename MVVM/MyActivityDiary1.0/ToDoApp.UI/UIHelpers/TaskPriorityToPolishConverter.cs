using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToDoApp.Core.Models.TaskItem;
using System.Windows.Data;

namespace ToDoApp.UI.UIHelpers
{
    public class TaskPriorityToPolishConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                TaskPriority.Low => "Zerowy",
                TaskPriority.BelowNormal => "Niski",
                TaskPriority.Normal => "Średni",
                TaskPriority.AboveNormal => "Ważny",
                TaskPriority.High => "Bardzo ważny",
                _ => "Nieznany"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
