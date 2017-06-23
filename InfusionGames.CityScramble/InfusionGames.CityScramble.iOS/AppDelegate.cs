using System;
using System.Diagnostics;
using WindowsAzure.Messaging;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using InfusionGames.CityScramble.Services;
using InfusionGames.CityScramble.ViewModels;
using UIKit;

namespace InfusionGames.CityScramble.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		#pragma warning disable 414
        private readonly CaliburnApplicationDelegate appDelegate = new CaliburnAppDelegate();
		#pragma warning restore 414

		private SBNotificationHub Hub { get; set;}
		private const string ConnectionString = "sb://infusionamazingracehub2-ns.servicebus.windows.net/";
		private const string NotificationHub = "infusionamazingracehub";
		private const string ConnectionStringKey = "cNQ2TN3stmOCbph9DhoB2LAaDRC9CbXI1rUKu0fyzfA=";

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			Debug.WriteLine("App finished launching");

			// Process any potential notification data from launch
			ProcessNotification(app.ApplicationState, options, true);

			// Register for Notifications
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
					   UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					   new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications();
			}
			else {
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			}

            global::Xamarin.Forms.Forms.Init();

            Xamarin.FormsMaps.Init();

            ImageCircleRenderer.Init();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new App(IoC.Get<SimpleContainer>()));

			#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
            #endif

            return base.FinishedLaunching(app, options);
        }

		public override void RegisteredForRemoteNotifications(UIApplication app, NSData deviceToken)
		{
			RegisterDeviceWithAppServer(deviceToken);

			// Connection string from your azure dashboard
			var cs = SBConnectionString.CreateListenAccess(new NSUrl(ConnectionString), ConnectionStringKey);

			// Register our info with Azure
			Hub = new SBNotificationHub(cs, NotificationHub);
			Hub.RegisterNativeAsync(deviceToken, null, err =>
			{
				if (err != null)
					Console.WriteLine("Error: " + err.Description);
				else
					Console.WriteLine("Success");
			});
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
			// TODO: Handle case where user starts app in airplane mode and fails to register for push
		}

		public override void ReceivedRemoteNotification(UIApplication app, NSDictionary userInfo)
		{
			// Process a notification received while the app was already open
			ProcessNotification(app.ApplicationState, userInfo, false);
		}

		public override void DidEnterBackground(UIApplication uiApplication)
		{
			Debug.WriteLine("App entered background");
			base.DidEnterBackground(uiApplication);
		}

		private void RegisterDeviceWithAppServer(NSData deviceToken)
		{
			// Get current device token
			var DeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace(DeviceToken))
			{
				DeviceToken = DeviceToken.Trim('<').Trim('>');
				DeviceToken = DeviceToken.Replace(" ", "");
			}

			// Save new device token 
			Settings.Current.DeviceRegistrationId = DeviceToken;
		}

		private void ProcessNotification(UIApplicationState appState, NSDictionary options, bool fromFinishedLaunching)
		{
			// Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
			if (null != options && options.ContainsKey(new NSString("aps")))
			{
				//Get the aps dictionary
				NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

				if (appState == UIApplicationState.Active)
				{
					if (aps.ContainsKey(new NSString("alert")))
					{
						var alert = (aps[new NSString("alert")] as NSString).ToString();
						//Manually show an alert
						if (!string.IsNullOrEmpty(alert))
						{
							UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
							avAlert.Show();
						}
					}

				}
				else 
				{
					if (options.ContainsKey(new NSString("clueId")))
					{
						var clueId = (options.ObjectForKey(new NSString("clueId")) as NSString).ToString();

						if (!string.IsNullOrEmpty(clueId))
						{
							var navigationService = IoC.Get<INavigationService>();
							// TODO Bootcamp: Navigate to details of clue
                            //navigationService.For<>()
								//.WithParam()
								//.Navigate(false);
						}
							
					}
				}
			}
		}
	}
}
