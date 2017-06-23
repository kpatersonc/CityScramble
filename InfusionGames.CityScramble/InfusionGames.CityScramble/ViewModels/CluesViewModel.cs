using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using InfusionGames.CityScramble.Behaviors;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.ViewModels
{
    /// <summary>
    /// Provides a list of current clues for a specific race
    /// </summary>
    public class CluesViewModel : BaseScreen, IRaceTab
    {
        #region IRaceTab implementation
        /// <summary>
        /// Navigation Parameter
        /// </summary>
        public Race SelectedRace { get; set; }

		public string Title { get; private set; } = "Clues";

		public string Icon { get; private set; } = "ic_clue.png";

        public int Priority => 3;

        public bool IsSupported(Race race)
        {
			return race.Enrolled && race.Status() != Race.ActiveStatus.Upcoming;
        }
        #endregion
    }
}
