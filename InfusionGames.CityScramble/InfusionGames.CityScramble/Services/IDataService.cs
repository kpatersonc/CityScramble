using System.Collections.Generic;
using System.Threading.Tasks;
using InfusionGames.CityScramble.Models;

namespace InfusionGames.CityScramble.Services
{
    public interface IDataService
    {

        Task<Profile> GetProfileAsync();

        Task<Team> JoinTeamAsync(string teamCode);

        Task<IEnumerable<Team>> GetTeams();

        Task<IEnumerable<Race>> GetRacesAsync();

        Task<Race> GetRaceAsync(string id);

        Task<IEnumerable<TeamClue>> GetCluesForTeamAsync(string raceId);

        Task<ClueResponse> GetClueResponse(string clueId);

        Task<ClueResponse> PostClueResponse(string clueId, double lat, double lng, byte[] dataArray, byte[] version);

        Task<bool> PostLocationUpdate(Location location);

        Task<string> RegisterForPushNotifications(string regId);

        Task<bool> UpdateDeviceInfoForPushNotifications(string regId, DeviceRegistration device);

        Task<bool> DeletePushRegistration(string regId);

    }
}
