namespace InfusionGames.CityScramble.Services
{
    /// <summary>
    /// An abstraction to decouple services from underlying setting storage mechanisms
    /// </summary>
    public interface ISettingsService
    {
        T GetValueOrDefault<T>(string key, T defaultValue = default(T));

        bool SetValue<T>(string key, T value);

        void Remove(string key);

        void ClearAll();

        void ClearTeamSettings();

        string RaceId { get; set; }

        string AzureRegistrationId { get; set; }

        string DeviceRegistrationId { get; set; }
        
        string UserName { get; set; }

        string MyTeamId { get; set; }

        string MyTeamName { get; set; }
    }
}
