namespace InfusionGames.CityScramble.Models
{
    public class Location : ILocation
    {
		public string RaceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
		public double Accuracy;
    }
}