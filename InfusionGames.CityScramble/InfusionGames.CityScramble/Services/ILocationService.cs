using System;
using System.Threading.Tasks;
using InfusionGames.CityScramble.Models;

namespace InfusionGames.CityScramble.Services
{
    public interface ILocationService
    {
        Task StartSendingLocation(Race race);

        Task StopSendingLocation();
    }
}
