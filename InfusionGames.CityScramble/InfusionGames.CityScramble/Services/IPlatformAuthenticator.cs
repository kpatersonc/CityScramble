using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace InfusionGames.CityScramble.Services
{
    public interface IPlatformAuthenticator
    {
        Task<MobileServiceUser> Authenticate(MobileServiceAuthenticationProvider provider);
        void Logout();
    }
}
