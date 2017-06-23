using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;

namespace InfusionGames.CityScramble.Droid.BackgroundServices
{
    [Service]
    class LocationBackgroundService : Service
    {
        private readonly string logTag = "LocationBackgroundService";

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

            Log.Debug(logTag, "Location Servce is running");
            
            //A sticky Service will be restarted, and a null intent will be delivered to OnStartCommand at restart.
            //Used when the Service is continuously performing a long-running operation, such as updating a stock feed.
            return StartCommandResult.Sticky; 
        }
        
        public override void OnDestroy()
        {
            base.OnDestroy();
            System.Diagnostics.Debug.WriteLine(logTag + "Location Service has been destroyed.");
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;   
        }
        
    }
}