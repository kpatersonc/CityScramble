using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.ViewModels
{
    /// <summary>
    /// Displays Leader board for specified race
    /// </summary>
    public class LeaderViewModel : BaseScreen, IRaceTab
    {
        #region IRaceTab implementation
        /// <summary>
        /// Navigation Parameter
        /// </summary>
        public Race SelectedRace { get; set; }

        public string Title { get; private set; } = "Leader Board";

        public string Icon { get; private set; } = "ic_leaderboard.png";

        public int Priority => 1;

        public bool IsSupported(Race race)
        {
            return true;
        } 
        #endregion
    }    
}
