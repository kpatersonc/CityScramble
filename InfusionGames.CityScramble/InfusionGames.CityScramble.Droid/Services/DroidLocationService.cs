using System.Threading.Tasks;
using Caliburn.Micro;
using InfusionGames.CityScramble.Droid.BackgroundServices;
using InfusionGames.CityScramble.Services;
using Plugin.Geolocator.Abstractions;
using InfusionGames.CityScramble.Models;
using System;

namespace InfusionGames.CityScramble.Droid.Services
{
    public class DroidLocationService : LocationService
    {
        public DroidLocationService(
            IGeolocator geoLocator,
            IDataService dataService,
            ISettingsService settingsService)
            : base(
                  geoLocator,
                  dataService,
                  settingsService)
        {
        }

        public override async Task StartSendingLocation(Race race)
        {
            throw new NotImplementedException("StartSendingLocation - start droid background service");

            await base.StartSendingLocation(race);
        }

        public override async Task StopSendingLocation()
        {
            await base.StopSendingLocation();

            throw new NotImplementedException("StopSendingLocation - stop droid background service");

        }

    }
}