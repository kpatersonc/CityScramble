using System;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace InfusionGames.CityScramble.ViewModels
{
    public abstract class BaseScreen : Screen
    {
        protected bool _isBusy;

        public virtual bool IsBusy
        {
            get { return _isBusy; }
            set { SetField(ref _isBusy, value); }
        }

        public bool IsOffline => false;

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Object.Equals(field,value))
            {
                field = value;
                NotifyOfPropertyChange(propertyName);
                return true;
            }
            return false;
        }

    }
}
