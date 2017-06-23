using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using InfusionGames.CityScramble.iOS.Services;
using InfusionGames.CityScramble.Services;
using InfusionGames.CityScramble.ViewModels;
using Xamarin.Auth;

namespace InfusionGames.CityScramble.iOS
{
    public class CaliburnAppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer _container;

        public CaliburnAppDelegate()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Instance(_container);

            _container.Singleton<App>();

            _container.Singleton<IMessageDialogService, TouchDialogService> ();
            _container.Singleton<IPlatformAuthenticator, TouchAuthenticator> ();
            _container.RegisterInstance(typeof(AccountStore), null, AccountStore.Create());
            _container.Singleton<IImageService, TouchImageService> ();
            _container.Singleton<ILocationService, LocationService>();
            _container.Singleton<IPushNotificationService, TouchPushNotificationService>();
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
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().Assembly,
                typeof(TabbedViewModel).Assembly
            };
        }

    }
}
