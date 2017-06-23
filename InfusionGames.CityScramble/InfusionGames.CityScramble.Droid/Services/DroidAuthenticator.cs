using System.Threading.Tasks;
using Android.Webkit;
using InfusionGames.CityScramble.Services;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Droid.Services
{
    public class DroidAuthenticator : IPlatformAuthenticator
    {
        private readonly IMobileServiceClient _client;

        public DroidAuthenticator(IMobileServiceClient client)
        {
            _client = client;
        }

        public async Task<MobileServiceUser> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            MobileServiceUser user = await _client.LoginAsync(Forms.Context, provider);

            return user;
        }

        public void Logout()
        {
            _client.Logout();
            // If the user chose to "Remember Me" while authenticating with google, then 
            // the credentials are stored in cookies. We need to clear the cookies 
            // to fully logout the user.
            CookieManager cookieManager = CookieManager.Instance;
            if (cookieManager != null)
            {
                cookieManager.RemoveAllCookie();
            }
            
        }
    }
}