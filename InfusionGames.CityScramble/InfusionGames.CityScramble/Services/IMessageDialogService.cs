using System.Threading.Tasks;

namespace InfusionGames.CityScramble.Services
{
    /// <summary>
    /// A simple mechanism to display alerts
    /// </summary>
    public interface IMessageDialogService
    {
        Task ShowAsync(string title, string message);
    }
}
