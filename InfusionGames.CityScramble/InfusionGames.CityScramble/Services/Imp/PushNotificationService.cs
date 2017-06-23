using System;
using System.Threading.Tasks;
using InfusionGames.CityScramble.Models;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Services
{
    public abstract class PushNotificationService : IPushNotificationService
    {
        protected readonly IDataService DataService;
        protected readonly ISettingsService SettingsService;

        protected PushNotificationService(IDataService dataService, ISettingsService settingService)
        {
            DataService = dataService;
            SettingsService = settingService;
        }

        /// <summary>
        /// Registers for push assuming device id has been set in Settings
        /// </summary>
        /// <returns></returns>
        public async Task RegisterForPush()
        {
            // Server will ensure that there is only one AzureRegId per DeviceRegId
            var azureRegId = await DataService.RegisterForPushNotifications(SettingsService.DeviceRegistrationId);

            if (!string.IsNullOrEmpty(azureRegId))
            {
                await DataService.UpdateDeviceInfoForPushNotifications(azureRegId, new DeviceRegistration()
                {
                    Handle = SettingsService.DeviceRegistrationId,
                    Platform = GetPlatformPushService(),
                });

                SettingsService.AzureRegistrationId = azureRegId;
            }
        }

        /// <summary>
        /// Deletes azure notification hub registration
        /// </summary>
        /// <returns></returns>
        public virtual async Task UnRegisterForPush()
        {
			if (!string.IsNullOrWhiteSpace(SettingsService.AzureRegistrationId))
            {
                await DataService.DeletePushRegistration(SettingsService.AzureRegistrationId);

                SettingsService.AzureRegistrationId = null;
            }
        }

        private string GetPlatformPushService()
        {
            switch (Device.OS)
            {
                case TargetPlatform.Android:
                    return "gcm";
                case TargetPlatform.iOS:
                    return "apns";
                default:
                    throw new NotImplementedException("Did not implement device registration for this platform " + Device.OS);
            }
        }

        
    }
}

