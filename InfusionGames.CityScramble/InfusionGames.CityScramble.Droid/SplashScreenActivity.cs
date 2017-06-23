using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace InfusionGames.CityScramble.Droid
{
    [Activity(Theme ="@style/MyTheme.Splash", MainLauncher =true, NoHistory = true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashScreenActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashScreenActivity.OnCreate");
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => {
                //Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
                Task.Delay(5000);  // Simulate a bit of startup work.
                //Log.Debug(TAG, "Working in the background - important stuff.");
            });

            startupWork.ContinueWith(t => {
                Log.Debug(TAG, "Work is finished - starting main activity.");
                StartActivity(new Intent(this.ApplicationContext, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}