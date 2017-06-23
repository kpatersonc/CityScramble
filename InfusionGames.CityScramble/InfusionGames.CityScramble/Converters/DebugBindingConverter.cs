using System;
using System.Globalization;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Converters
{
    public class DebugBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // place debug breakpoint here!
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
