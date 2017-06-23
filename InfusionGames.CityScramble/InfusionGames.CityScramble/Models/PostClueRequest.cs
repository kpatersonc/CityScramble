using System;

namespace InfusionGames.CityScramble.Models
{
    public class PostClueRequest
    {
        public string Data { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte[] Version { get; set; }
    }
}
