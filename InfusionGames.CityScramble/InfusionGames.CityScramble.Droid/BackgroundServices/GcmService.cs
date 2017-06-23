using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Android.Util;
using Caliburn.Micro;
using Gcm.Client;
using InfusionGames.CityScramble.Services;

[assembly: Permission(Name = "com.infusion.games.cityscramble.droid.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.infusion.games.cityscramble.droid.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
namespace InfusionGames.CityScramble.Droid.BackgroundServices
{
    [Service]
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }

        public GcmService() : base(PushHandlerBroadcastReceiver.SENDER_IDS)
        {
            
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
            RegistrationID = registrationId;

            // Save the registration id, will be used later to register with backend.
            Settings.Current.DeviceRegistrationId = RegistrationID;
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info("PushHandlerBroadcastReceiver", "GCM Message Received!");

            string msg2 = intent.Extras.GetString("msg");
            string clueId = null;
            if (intent.Extras.ContainsKey("clueId"))
            {
                clueId = intent.Extras.GetString("clueId");
            }
            if (!string.IsNullOrEmpty(msg2))
            {
                // Show a dialog if app in foreground.
                if (AppInForeground(context))
                {
                    var dialogService = IoC.Get<IMessageDialogService>();
                    MainActivity.CurrentActivity.RunOnUiThread(() =>
                    {
                        dialogService.ShowAsync("City Scramble", msg2);
                    });
                }
                else
                {
                    CreateNotification("City Scramble", msg2, clueId);
                }
            }
        }


        public bool AppInForeground(Context context)
        {
            ActivityManager activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);
            IList<Android.App.ActivityManager.RunningAppProcessInfo> appProcesses = activityManager.RunningAppProcesses;
            if (appProcesses == null)
            {
                return false;
            }
            string packageName = context.PackageName;
            foreach (Android.App.ActivityManager.RunningAppProcessInfo appProcess in appProcesses)
            {
                if (appProcess.Importance == Importance.Foreground && appProcess.ProcessName == packageName)
                {
                    return true;
                }
            }
            return false;
        }

        void CreateNotification(string title, string desc, string clueId = null)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show ui
            var uiIntent = new Intent(this, typeof(MainActivity));
            uiIntent.AddFlags(ActivityFlags.ClearTop);
            if (!string.IsNullOrEmpty(clueId))
            {
                uiIntent.PutExtra("clueid", clueId);
            }
            //Use Notification Builder
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            //Create the notification
            //we use the pending intent, passing our ui intent over which will get called
            //when the notification is tapped.
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, PendingIntentFlags.OneShot))
                    .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                    .SetTicker(title)
                    .SetContentTitle(title)
                    .SetContentText(desc)
                    
                    //Set the notification sound
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))

                    //Auto cancel will remove the notification once the user touches it
                    .SetAutoCancel(true).Build();

            //Show the notification
            notificationManager.Notify(1, notification);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
        }

    }

    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { "186815306449" }; // Google API Project Number
        public const string ListenConnectionString = "Endpoint=sb://infusionamazingracehub2-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=cNQ2TN3stmOCbph9DhoB2LAaDRC9CbXI1rUKu0fyzfA=";
        public const string NotificationHubName = "infusionamazingracehub";
        public const string tag = "test";
    }
}