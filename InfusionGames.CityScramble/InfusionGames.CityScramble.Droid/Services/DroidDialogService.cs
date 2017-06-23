using System.Threading.Tasks;
using Android.App;
using InfusionGames.CityScramble.Services;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Droid.Services
{
    public class DroidDialogService : IMessageDialogService
    {
        public async Task ShowAsync(string title, string message)
        {
            var messageTcs = new TaskCompletionSource<bool>();

            AlertDialog.Builder builder = new AlertDialog.Builder(Forms.Context);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetPositiveButton("Ok", (sender, args) =>
            {
                messageTcs.SetResult(true);
            });

            builder.Create().Show();

            await messageTcs.Task;
        }
    }
}