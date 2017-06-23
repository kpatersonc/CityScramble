using InfusionGames.CityScramble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace InfusionGames.CityScramble.UWP.Services
{
    public class UwpPushNotificationService : PushNotificationService
    {
        public UwpPushNotificationService(IEventAggregator eventAggregator, IDataService dataService, ISettingsService settingService) : 
            base(eventAggregator, dataService, settingService)
        {
        }

        protected override Task UnRegisterForPush()
        {
            return base.UnRegisterForPush();
        }
    }
}
