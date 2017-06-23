using System.Threading.Tasks;
using Caliburn.Micro;
using Gcm;
using InfusionGames.CityScramble.Services;

namespace InfusionGames.CityScramble.Droid.Services
{
    public class DriodPushNotificationService : PushNotificationService
    {

        public DriodPushNotificationService(IDataService dataService, ISettingsService settingService) 
            : base(dataService, settingService)
        {
        }

        public override async Task UnRegisterForPush()
        {
            await base.UnRegisterForPush();

            GcmClient.UnRegister(Android.App.Application.Context);
        }

    }
}