using System;
using System.Collections.Generic;
using System.Reflection;
using Android.Runtime;
using Caliburn.Micro;
using InfusionGames.CityScramble.Droid.Services;
using InfusionGames.CityScramble.Services;
using InfusionGames.CityScramble.ViewModels;
using Xamarin.Auth;

namespace InfusionGames.CityScramble.Droid
{
    public class Application : CaliburnApplication
    {
        private SimpleContainer _container;

        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            AppDomain appDomain = AppDomain.CurrentDomain;
            appDomain.UnhandledException += AppDomainOnUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            Initialize();
        }

        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
            //MainActivity.CurrentActivity.RunOnUiThread(() =>
            //{
            //    AlertDialog.Builder builder = new AlertDialog.Builder(Forms.Context);
            //    builder.SetTitle("Unhandled Exception Occurred");
            //    builder.SetMessage("If you see this, take a screenshot.. maybe you'll get bonus points. " + e.Exception.Message);
            //    builder.Show();
            //});
        }

        private void AppDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs ex)
        {
            
            //MainActivity.CurrentActivity.RunOnUiThread(() =>
            //{
            //    AlertDialog.Builder builder = new AlertDialog.Builder(Forms.Context);
            //    builder.SetTitle("Unhandled Exception Occurred");
            //    builder.SetMessage("If you see this, take a screenshot.. maybe you'll get bonus points. " + ex.ExceptionObject);
            //    builder.Show();
            //});
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Instance(_container);

            _container.Singleton<App>();

            _container.Singleton<IMessageDialogService, DroidDialogService>();
            _container.Singleton<IPlatformAuthenticator, DroidAuthenticator>();
            _container.RegisterInstance(typeof(AccountStore), null, AccountStore.Create());
            _container.Singleton<IImageService, DroidImageService>();
            _container.Singleton<ILocationService, DroidLocationService>();
            _container.Singleton<IPushNotificationService, DriodPushNotificationService>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().Assembly,
                typeof (LoginViewModel).Assembly
            };
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
    }
}