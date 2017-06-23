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
using System.Collections.Generic;

namespace InfusionGames.CityScramble.ViewModels
{
    /// <summary>
    /// Provides a list of current clues for a specific race
    /// </summary>
    public class CluesViewModel : BaseScreen, IRaceTab
    {
        private readonly IDataService _dataService;

        public CluesViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Clues = new BindableCollection<TeamClue>();
        }
        #region IRaceTab implementation
        /// <summary>
        /// Navigation Parameter
        /// </summary>
        public Race SelectedRace { get; set; }

		public string Title { get; private set; } = "Clues";

		public string Icon { get; private set; } = "ic_clue.png";

        public int Priority => 3;


        private BindableCollection<TeamClue> _clues;
        public BindableCollection<TeamClue> Clues { get; protected set; }


        public bool IsSupported(Race race)
        {
			return race.Enrolled && race.Status() != Race.ActiveStatus.Upcoming;
        }
        #endregion

        protected override async void OnActivate()
        {
            base.OnActivate();
            IsBusy = true;
            IEnumerable<TeamClue> localClues = await _dataService.GetCluesForTeamAsync(SelectedRace.Id);
            foreach (TeamClue cl in localClues)
            {
                Clues.Add(cl);
            }
            var a = 1;
            IsBusy = false;
        }

        protected override async void OnInitialize()
        {
            IsBusy = true;
            base.OnInitialize();

            IEnumerable<TeamClue> localClues = await _dataService.GetCluesForTeamAsync(SelectedRace.Id);
            foreach (TeamClue cl in localClues)
            {
                Clues.Add(cl);
            }
            var a = 1;
            IsBusy = false;
        }
    }
}
