using System;
using System.Threading.Tasks;
using InfusionGames.CityScramble.Services;
using Windows.UI.Popups;

namespace InfusionGames.CityScramble.UWP
{
    public class UwpDialogService : IMessageDialogService
    {
        public async Task ShowAsync(string title, string message)
        {
            var dialog = new MessageDialog(message, title);
            await dialog.ShowAsync();
        }
    }
}