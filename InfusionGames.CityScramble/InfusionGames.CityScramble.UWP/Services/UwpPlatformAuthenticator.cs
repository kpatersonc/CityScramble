using InfusionGames.CityScramble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Windows.Security.Authentication.Web;

namespace InfusionGames.CityScramble.UWP.Services
{
    class UwpPlatformAuthenticator : IPlatformAuthenticator
    {
        private IMobileServiceClient _client;

        public UwpPlatformAuthenticator(IMobileServiceClient client)
        {
            _client = client;
        }

        public async Task<MobileServiceUser> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            try
            {
                return await _client.LoginAsync(provider);                
            }
            catch
            {
                // CRAP!
                return null;
            }
        }

        public void Logout()
        {
            // no-op
        }
    }
}
