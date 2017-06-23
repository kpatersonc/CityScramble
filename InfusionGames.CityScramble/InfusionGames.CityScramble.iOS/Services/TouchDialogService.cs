using System;
using System.Threading.Tasks;
using InfusionGames.CityScramble.Services;
using UIKit;

namespace InfusionGames.CityScramble.iOS.Services
{
    public class TouchDialogService : IMessageDialogService
    {
        public async Task ShowAsync (string title, string message)
        {
            var messageTcs = new TaskCompletionSource<bool> ();

            var alert = new UIAlertView
            {
                Title = title,
                Message = message
            };

            alert.AddButton ("OK");

            EventHandler<UIButtonEventArgs> setResultHandler = null;

            setResultHandler = (sender, e) => 
            {
                messageTcs.SetResult (true);
                alert.Clicked -= setResultHandler;
            };

            alert.Clicked += setResultHandler;

            alert.Show ();

            await messageTcs.Task;
        }
    }
}