using System;
using System.Globalization;
using System.Windows.Data;

namespace AstronomyTesting.WPF.Converters
{
    [ValueConversion(typeof(bool), typeof(bool?))]
    public class BoolToNullableBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool?)System.Convert.ToBoolean(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? (bool)value : false;
        }
    }
}
