namespace InfusionGames.CityScramble.Models
{
    /// <summary>
    /// Device Registration
    /// </summary>
    public class DeviceRegistration
    {
        /// <summary>
        /// Specifies which platform for this device (gcm is assumed by default)
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Specifies the registration id of the device (eg, gcm registration)
        /// </summary>
        public string Handle { get; set; }

        /// <summary>
        /// Specifies the tags the user is interested in receiving
        /// </summary>
        public string[] Tags { get; set; }
    }
}
