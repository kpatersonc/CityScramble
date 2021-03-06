﻿using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using InfusionGames.CityScramble.Controls;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;
using InfusionGames.CityScramble.Views;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

namespace InfusionGames.CityScramble.ViewModels
{
    public class MapViewModel : BaseScreen, IRaceTab
    {
        #region IRaceTab implementation

        private IDataService _dataService;

        public MapViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public Race SelectedRace { get; set; }

		public string Title { get; private set; } = "Map";

		public string Icon { get; private set; } = "ic_location.png";

        public int Priority => 4;

        public bool IsSupported(Race race)
        {
            return race.Enrolled && race.Status() != Race.ActiveStatus.Upcoming;
        }

        #endregion

        #region Overrides

        protected override async void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            // TODO: Acquire the map from the view because the map control isn't MVVM friendly

            var mapView = (MapView)view;
            var map = mapView.Map;
            var clueCords = new List<Position>();
            var clues = await _dataService.GetCluesForTeamAsync(SelectedRace.Id);
            foreach(var clue in clues)
            {
                var pin = new Pin();
                
                pin.Position = new Position(clue.Latitude, clue.Longitude);
                var customPin = new CustomPin(pin, "Pin");
                map.AddCustomPin(customPin);
                clueCords.Add(pin.Position);
            }
            map.MoveToRegion(MapSpan.FromCenterAndRadius(GetCentralGeoCoordinate(clueCords), Distance.FromKilometers(1)));



        }

        #endregion
        

        /// <summary>
        /// Return the geo center of all points.
        /// </summary>
        /// <param name="geoCoordinates">List of goe locations.</param>
        /// <returns></returns>
        public static Position GetCentralGeoCoordinate(IList<Position> geoCoordinates)
        {
            if (geoCoordinates.Count == 1)
            {
                return geoCoordinates.Single();
            }

            double maxLat = -85;
            double minLat = 85;
            double maxLong = -180;
            double minLong = 180;

            foreach (var geoCoordinate in geoCoordinates)
            {
                if (geoCoordinate.Latitude > maxLat)
                {
                    maxLat = geoCoordinate.Latitude;
                }
                if (geoCoordinate.Latitude < minLat)
                {
                    minLat = geoCoordinate.Latitude;
                }
                if (geoCoordinate.Longitude > maxLong)
                {
                    maxLong = geoCoordinate.Longitude;
                }
                if (geoCoordinate.Longitude < minLong)
                {
                    minLong = geoCoordinate.Longitude;
                }
            }

            var lat = (maxLat + minLat) / 2;
            var lng = (maxLong + minLong) / 2;            

            return new Position(lat,lng);
        }

	}
}
