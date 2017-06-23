using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Gcm.Client;
using InfusionGames.CityScramble.Droid.BackgroundServices;
using InfusionGames.CityScramble.ViewModels;
using Java.Lang;
using Xamarin.Forms.Platform.Android;

namespace InfusionGames.CityScramble.Droid
{
    [Activity(
        Label = "City Scramble", 
        Icon = "@drawable/icon", 
        Theme = "@style/CityScrambleTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    )]
    public class MainActivity : FormsApplicationActivity
    {

        static MainActivity instance = null;

        // Return the current activity instance.
        public static MainActivity CurrentActivity => instance;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(IoC.Get<App>());

            try
            {
                // Check to ensure everything's setup right
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);

                // Register for push notifications
                System.Diagnostics.Debug.WriteLine("Registering...");
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog(this, "There was an error creating the Mobile Service. Verify the URL", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(this, e.Message, "Error");
            }

            instance = this;

            if (Intent.HasExtra("clueid"))
            {
                var navigationService = IoC.Get<INavigationService>();
                // TODO Bootcamp: Navigate to details of clue
                //navigationService.For<>()
                    //.WithParam()
                    //.Navigate(false);
            }
        }

        public static void CreateAndShowDialog(Context context, string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}