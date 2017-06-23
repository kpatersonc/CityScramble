using System.Threading.Tasks;

namespace InfusionGames.CityScramble.Services
{
    /// <summary>
    /// Provides a simple wrapper around Authentication components
    /// </summary>
    public interface IAuthenticationService
    {

		Task<bool> IsUserLoggedInWithTeamAsync();

        Task<bool> IsLoggedInAsync();

        Task<bool> LoginAsync();

        Task LogoutAsync();

    }
}
