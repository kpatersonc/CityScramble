using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace InfusionGames.CityScramble.Services
{
    public class SettingsService : ISettingsService
    {
        #region Setting Constants

        private const string RaceIdKey = "race_id";
        private static readonly string RaceIdDefault = string.Empty;

        private const string AzureRegIdKey = "azure_registration";
        private static readonly string AzureRegIdDefault = string.Empty;


        private const string DeviceRegIdKey = "device_registration";
		private static readonly string DeviceRegIdDefault = string.Empty;

        private const string UserNameKey = "user_name";
        private static readonly string UserNameDefault = string.Empty;

        private const string MyTeamIdKey = "my_team_id";
        private static readonly string MyTeamIdDefault = string.Empty;

        private const string MyTeamNameKey = "my_team_name";
        private static readonly string MyTeamNameDefault = string.Empty;

        #endregion

        private ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public string RaceId
        {
            get { return AppSettings.GetValueOrDefault<string>(RaceIdKey, RaceIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(RaceIdKey, value); }
        }

        public string AzureRegistrationId
        {
            get { return AppSettings.GetValueOrDefault<string>(AzureRegIdKey, AzureRegIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AzureRegIdKey, value); }
        }

        public string DeviceRegistrationId
        {
            get { return AppSettings.GetValueOrDefault<string>(DeviceRegIdKey, DeviceRegIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(DeviceRegIdKey, value); }
        }

        public string UserName
        {
            get { return AppSettings.GetValueOrDefault<string>(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserNameKey, value); }
        }

        public string MyTeamId
        {
            get { return AppSettings.GetValueOrDefault<string>(MyTeamIdKey, MyTeamIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(MyTeamIdKey, value); }
        }

        public string MyTeamName
        {
            get { return AppSettings.GetValueOrDefault<string>(MyTeamNameKey, MyTeamNameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(MyTeamNameKey, value); }
        }

        public T GetValueOrDefault<T>(string key, T defaultValue = default(T))
        {
            return AppSettings.GetValueOrDefault(key, defaultValue);
        }

        public void Remove(string key)
        {
            AppSettings.Remove(key);
        }

        public bool SetValue<T>(string key, T value)
        {
            return AppSettings.AddOrUpdateValue(key, value);
        }

        public void ClearTeamSettings()
        {
            Remove(MyTeamIdKey);
            Remove(MyTeamNameKey);
        }

        public void ClearAll()
        {
			ClearTeamSettings();
            Remove(RaceIdKey);
            Remove(UserNameKey);
            Remove(AzureRegIdKey);
        }
    }

}
