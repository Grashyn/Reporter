using ReporterWPF.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ReporterWPF.Converters
{
    public class WindowStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState state)
            {
                var parameterState = (WindowState)Enum.Parse(typeof(WindowState), (string)parameter);
                return parameterState == state ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }

            throw new ArgumentException(@"Parameter is invalid", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
