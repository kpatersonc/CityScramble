using System.Threading.Tasks;
using Caliburn.Micro;
using InfusionGames.CityScramble.Services;
using UIKit;

namespace InfusionGames.CityScramble.iOS.Services
{
    public class TouchPushNotificationService : PushNotificationService
    {
        public TouchPushNotificationService(IDataService dataService, ISettingsService settingService)
            : base(dataService, settingService)
        {

        }

        public override async Task UnRegisterForPush()
        {
            await base.UnRegisterForPush();

            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
        }
    }
}

