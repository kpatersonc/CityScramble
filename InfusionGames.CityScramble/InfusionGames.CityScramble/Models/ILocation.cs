using System;

namespace InfusionGames.CityScramble.Models
{
	public interface ILocation
	{
		double Latitude { get; set; }
		double Longitude { get; set; }
	}

	public static class LocationExtensions 
	{
		public static double CalculateDistanceTo(this ILocation locationFrom, ILocation locationTo)
		{
			return locationFrom.CalculateDistanceTo(locationTo.Latitude, locationTo.Longitude);
		}

		public static double CalculateDistanceTo(this ILocation locationFrom, double latitudeTo, double longitudeTo)
		{
			var R = 6371; // km
			var distanceLat = (locationFrom.Latitude - latitudeTo).ToRad();
			var distanceLng = (locationFrom.Longitude - longitudeTo).ToRad();
			latitudeTo = latitudeTo.ToRad();
			longitudeTo = longitudeTo.ToRad();

			var a = Math.Sin(distanceLat / 2) * Math.Sin(distanceLat / 2) +
					Math.Sin(distanceLng / 2) * Math.Sin(distanceLng / 2) * Math.Cos(latitudeTo) * Math.Cos(longitudeTo);

			var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

			return R * c;
		}

		public static double ToRad(this double target)
		{
			return target * (Math.PI / 180);
		}
	}
}