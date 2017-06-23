using System;
using System.Diagnostics;
using System.Threading.Tasks;
using InfusionGames.CityScramble.Models;
using Plugin.Geolocator.Abstractions;

namespace InfusionGames.CityScramble.Services
{
    public class LocationService : ILocationService
    {
        private readonly IGeolocator _geoLocator;
        private readonly IDataService _dataService;
        private readonly ISettingsService _settingsService;

        private EventHandler<PositionEventArgs> _positionChangeHandler;
        private readonly TimeSpan _maxQuietTime = TimeSpan.FromMinutes(2);

        private DateTime _lastUpdatedTime = DateTime.MinValue;
        private Location _previousLocation;
        private Race _race;

        public LocationService(IGeolocator geoLocator, IDataService dataService, ISettingsService settingsService)
        {
            _geoLocator = geoLocator;
            _dataService = dataService;
            _settingsService = settingsService;
        }

        public virtual async Task StartSendingLocation(Race race)
        {
            _race = race;

            if (!_race.IsActive() || !_geoLocator.IsGeolocationEnabled)
            {
                await StopSendingLocation();
            }
            else
            {
                _geoLocator.DesiredAccuracy = 5; // meters
                _geoLocator.AllowsBackgroundUpdates = true;

                await CleanUp();

                _positionChangeHandler = async (sender, e) =>
                {
                    var location = new Location
                    {
                        RaceId = _settingsService.RaceId,
                        Latitude = e.Position.Latitude,
                        Longitude = e.Position.Longitude,
                        Accuracy = e.Position.Accuracy
                    };

                    var msg = $"Location received {DateTime.Now - _lastUpdatedTime}: {location.Latitude},{location.Longitude} {location.Accuracy}";
                    Debug.WriteLine(msg);

                    await SendLocation(location);
                };

                _geoLocator.PositionChanged += _positionChangeHandler;

                int seconds = 5;
                await _geoLocator.StartListeningAsync(seconds*1000, _geoLocator.DesiredAccuracy);
            }
        }

        public virtual async Task StopSendingLocation()
        {
            await CleanUp();
        }

        protected async Task CleanUp()
        {
            _lastUpdatedTime = DateTime.MinValue;
            _previousLocation = null;

            if (_positionChangeHandler != null)
            {
                _geoLocator.PositionChanged -= _positionChangeHandler;
                _positionChangeHandler = null;
            }
            if (_geoLocator.IsListening)
            {
                await _geoLocator.StopListeningAsync();
            }
        }

        private async Task SendLocation(Location location)
        {
            if (await ShouldSendLocation(location))
            {
                try
                {
                    bool result = await _dataService.PostLocationUpdate(location);
                    if (!result)
                    {
                        Debug.WriteLine("Location update was not successful.");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception occurred while sending location: " + e.ToString());
                }
            }
        }

        private async Task<bool> ShouldSendLocation(Location location)
        {
            if (!_race.IsActive())
            {
                await StopSendingLocation();
                return false;
            }

            if (_previousLocation != null)
            {
                // Do not send location update if distance to previously sent location is
                // less then half the accuracy of the current update and we are within the MaxQuietTime
                double distance = location.CalculateDistanceTo(_previousLocation);

                if (distance < location.Accuracy * 0.5 && _maxQuietTime > (DateTime.Now - _lastUpdatedTime))
                {
                    return false;
                }
            }

            var msg = string.Format("{0}: {1},{2} {3}",
                _lastUpdatedTime == DateTime.MinValue ? "First Location " : (DateTime.Now - _lastUpdatedTime).ToString(),
                location.Latitude, location.Longitude, location.Accuracy);

            Debug.WriteLine(msg);

            _previousLocation = location;

            _lastUpdatedTime = DateTime.Now;

            return true;
        }

    }
}