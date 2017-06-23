using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Converters
{
    public class OnPlatform<T> : Xamarin.Forms.OnPlatform<T>
    {
        public T Default { get; set; }

        public T Windows { get; set; }

        public static implicit operator T(OnPlatform<T> onPlatform)
        {

            if (Device.OS == TargetPlatform.Windows && HasValue(onPlatform.Windows))
            {
                return onPlatform.Windows;
            }

            T value = (Xamarin.Forms.OnPlatform<T>)onPlatform;
            if (HasValue(value))
                return value;

            return onPlatform.Default;
        }

        private static bool HasValue(T value)
        {
            return !(EqualityComparer<T>.Default.Equals(value, default(T)));
        }
    }
}
