using Android.Content;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Droid.Services
{
    /// <summary>
    /// Broadcast monitor.
    /// </summary>
    public abstract class BroadcastMonitor : BroadcastReceiver
    {
        /// <summary>
        ///  Start monitoring. 
        /// </summary>
        public bool Start()
        {
            var intent = Forms.Context.RegisterReceiver(this, Filter);
            return intent != null;
        }

        /// <summary>
        ///  Stop monitoring. 
        /// </summary>
        public void Stop()
        {
            Forms.Context.UnregisterReceiver(this);
        }

        /// <summary>
        /// Gets the intent filter to use for monitoring.
        /// </summary>
        protected abstract IntentFilter Filter { get; }
    }
}