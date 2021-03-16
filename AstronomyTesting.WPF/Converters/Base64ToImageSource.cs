using Converters;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AstronomyTesting.WPF.Converters
{
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class Base64ToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitmapConverter.GetImageSource(System.Convert.FromBase64String(System.Convert.ToString(value)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBase64String(BitmapConverter.GetBuffer((ImageSource)value));
        }
    }
}
