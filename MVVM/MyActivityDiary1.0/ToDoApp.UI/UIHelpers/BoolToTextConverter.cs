using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ToDoApp.UI.UIHelpers
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? "Tak" : "Nie";

            return "Nie";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString() == "Tak";
    }
}
