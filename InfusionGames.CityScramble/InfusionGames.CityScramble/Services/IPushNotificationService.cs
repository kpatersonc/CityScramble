using System;
using System.Threading.Tasks;

namespace InfusionGames.CityScramble.Services
{
    public interface IPushNotificationService
    {
        Task RegisterForPush();
        Task UnRegisterForPush();
    }
}
