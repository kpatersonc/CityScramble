using Caliburn.Micro;
using ImageCircle.Forms.Plugin.Abstractions;
using ImageCircle.Forms.Plugin.UWP;
using InfusionGames.CityScramble.Services;
using InfusionGames.CityScramble.UWP.Services;
using InfusionGames.CityScramble.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Xamarin.Auth;

namespace InfusionGames.CityScramble.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        private WinRTContainer _container;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
                DebugSettings.EnableFrameRateCounter = true;
#endif

            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                // hide windows phone status bar
                if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
                {
                    var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                    await statusBar.HideAsync();
                }

                Initialize(); // init Caliburn.Micro
                Xamarin.Forms.Forms.Init(e, RendererAssemblies());
                Xamarin.FormsMaps.Init("f6ZrO7ow0zlKXBd4Deoc~fhcElURWlNJgdoAwdr4OPQ~AoRpcSZYX6h0LfgJaqLM36LiM02-x3TizVw6ygEuu4l4NiP9Js85b6TCh0vSszaO"); // UWP 10 Key for City.Scramble.Dev obtained from Bing Maps web site https://www.bingmapsportal.com/Application
                ImageCircleRenderer.Init();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                // todo: DisplayRootView?
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
            //if (e.PreviousExecutionState == ApplicationExecutionState.Running)
            //    return;

            //Xamarin.Forms.Forms.Init(e);
            //ImageCircleRenderer.Init();

            //DisplayRootView<MainPage>();
        }

        protected override void PrepareViewFirst()
        {
            base.PrepareViewFirst();
        }

        protected override Frame CreateApplicationFrame()
        {
            return base.CreateApplicationFrame();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            base.PrepareViewFirst(rootFrame);
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
        }

        #region Caliburn IOC configuration and overloads
        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterInstance(typeof(WinRTContainer), null, _container);
            _container.RegisterInstance(typeof(SimpleContainer), null, _container);

            _container.Singleton<InfusionGames.CityScramble.App>();

            _container.Singleton<IMessageDialogService, UwpDialogService>();
            _container.Singleton<IPlatformAuthenticator, UwpPlatformAuthenticator>();
            _container.Instance<AccountStore>(new UwpAccountStore());
            _container.Singleton<IImageService, UwpImageService>();
            //_container.Singleton<IApplicationService, LocationService>();
            //_container.Singleton<IApplicationService, TouchPushNotificationService>();
            //_container.Singleton<INetworkService, UwpNetworkService>();

        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            try
            {
                return _container.GetInstance(service, key);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to resolve service '{0}' with key '{1}'.", service.Name, key), ex);
            }
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().GetTypeInfo().Assembly,
                typeof(LoginViewModel).GetTypeInfo().Assembly
            };
        }
        #endregion

        #region Lifetime events
        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
        }

        protected override void OnResuming(object sender, object e)
        {
            base.OnResuming(sender, e);
        }

        protected override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            base.OnSuspending(sender, e);
        }

        protected override void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.Exception);
            base.OnUnhandledException(sender, e);
        }
        #endregion

        private IEnumerable<Assembly> RendererAssemblies()
        {
            return new[]
            {
                typeof(ImageCircleRenderer).GetTypeInfo().Assembly,
                typeof(CircleImage).GetTypeInfo().Assembly,
            };
        }
    }
}
