namespace InfusionGames.CityScramble.Models
{
    public class PutClueResponse
    {
        public string Id { get; set; }
        public string EndpointUri { get; set; }
        public string ContainerName { get; set; }
        public string ResourceName { get; set; }
        public string SasQueryString { get; set; }
        public byte[] Version { get; set; }
    }
}
