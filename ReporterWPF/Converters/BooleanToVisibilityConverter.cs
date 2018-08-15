using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ReporterWPF.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private const string InvertParameter = "invert";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                var invertParameter = parameter as string;
                bool boolValue;
                if (invertParameter == InvertParameter)
                {
                    boolValue = !b;
                }
                else
                {
                    boolValue = b;
                }

                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }

            throw new ArgumentException(@"Parameter is invalid", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
