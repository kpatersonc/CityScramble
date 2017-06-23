using System;
using System.Globalization;
using InfusionGames.CityScramble.Models;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Converters
{
    class RaceDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var race = value as Race;
            if (race == null)
                return String.Empty;

            if (!race.StartDate.HasValue)
                return String.Empty;

            if (!race.EndDate.HasValue)
                return String.Empty;

            var now = DateTimeOffset.UtcNow.ToLocalTime();
            var raceStart = race.StartDate.Value.ToLocalTime();
            var raceEnd = race.EndDate.Value.ToLocalTime();
            
            // future
            if (raceStart > now)
            {
                // just use the start date
                return raceStart.ToString("D");
            }
            // previous
            else if (raceEnd < now)
            {
                // use the end date
                return raceEnd.ToString("D");
            }
            // current
            else
            {
                // if it starts and ends on the same day
                if (raceStart.Date == raceEnd.Date)
                {
                    // show the time
                    return String.Format("{0} - {1}", raceStart.ToString("t"), raceEnd.ToString("t"));
                }
                else
                {
                    // show dates
                    return String.Format("{0} - {1}", raceStart.ToString("M"), raceEnd.ToString("M"));
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
