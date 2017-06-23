using System.Linq;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using InfusionGames.CityScramble.Services;
using InfusionGames.CityScramble.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace InfusionGames.CityScramble
{
    public partial class App : FormsApplication
    {
        //DEV
        private static string MobileServicesUri = "https://infusionamazingrace.azure-mobile.net/";
        private static string MobileServicesAppKey = "XlYlBNtwuFzFROAbFpblbBQEPmdPJg64";

        private readonly SimpleContainer container;

        public App(SimpleContainer container)
        {
            InitializeComponent();            

            this.container = container;

			container
				.PerRequest<LoginViewModel>()
				.PerRequest<RaceSelectionViewModel>()
				.PerRequest<TabbedViewModel>()
                .PerRequest<JoinTeamViewModel>()
				.PerRequest<IRaceTab, LeaderViewModel>()
				.PerRequest<IRaceTab, CluesViewModel>()
				.PerRequest<IRaceTab, MapViewModel>()
				.PerRequest<MapViewModel>()
				.PerRequest<SubmitClueViewModel>()
                ;

            var client = new MobileServiceClient(MobileServicesUri, MobileServicesAppKey);

            container.Singleton<ISettingsService, SettingsService>();
            container.Instance<IMobileServiceClient>(client);
            container.Singleton<IAuthenticationService, AuthenticationService>();
            container.Singleton<IDataService, DataService>();
            container.Instance(CrossGeolocator.Current);
            container.Singleton<ViewPresenter>();

            // ask the viewpresenter to display the first view (synchronously)
            IoC.Get<ViewPresenter>().ShowStartPage();
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
