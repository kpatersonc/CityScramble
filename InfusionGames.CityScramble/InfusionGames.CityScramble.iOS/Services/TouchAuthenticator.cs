using System.Threading.Tasks;
using Foundation;
using InfusionGames.CityScramble.Services;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace InfusionGames.CityScramble.iOS.Services
{
    public class TouchAuthenticator : IPlatformAuthenticator
    {
        private readonly IMobileServiceClient _client;

        public TouchAuthenticator (IMobileServiceClient client)
        {
            _client = client;
        }

        public async Task<MobileServiceUser> Authenticate (MobileServiceAuthenticationProvider provider)
        {
            MobileServiceUser user = await _client.LoginAsync (UIApplication.SharedApplication.KeyWindow.RootViewController, provider);

            return user;
        }

        public void Logout()
        {
            _client.Logout();
            // If the user chose to "Remember Me" while authenticating with google, then 
            // the credentials are stored in cookies. We need to clear the cookies 
            // to fully logout the user.
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }
            
        }
    }
}