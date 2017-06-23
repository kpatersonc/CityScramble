using System;
using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;
using Xamarin.Forms;
using Caliburn.Micro.Xamarin.Forms;

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
        /// 
        private readonly IDataService _dataService;
        private readonly INavigationService _navService;
        private readonly ISettingsService _setService;

        public LeaderViewModel(IDataService dataService, INavigationService navService, ISettingsService setService)
        {
            _dataService = dataService;
            _navService = navService;
            _setService = setService;
            Teams = new BindableCollection<Team>();
           
        }


        private string _ourTeamName;
        public string OurTeamName
        {
            get { return _ourTeamName; }
            set { SetField(ref _ourTeamName, value); }
        }

        private int _ourTeamPoints;
        public int OurTeamPoints
        {
            get { return _ourTeamPoints; }
            set { SetField(ref _ourTeamPoints, value); }
        }

        public Race SelectedRace { get; set; }

        public string Title { get; private set; } = "Leader Board";

        public string Icon { get; private set; } = "ic_leaderboard.png";

        public int Priority => 1;

        

    private BindableCollection<Team> _teams;
        public BindableCollection<Team> Teams { get; protected set; }

        public bool IsSupported(Race race)
        {
            return true;
        }

        protected override async void OnActivate()
        {
            var race = await _dataService.GetRaceAsync(SelectedRace.Id);
            //base.OnActivate();
            IsBusy = true;
            var sortedTeams = race.Teams.OrderByDescending(t => t.Points).Take(3);
            foreach (var team in sortedTeams)
            {
                Teams.Add(team);
                if(team.Name == _setService.MyTeamName)
                {
                    OurTeamName = team.Name;
                    OurTeamPoints = team.Points;
                }
            }
            IsBusy = false;
        }

        protected override async void OnInitialize()
        {
            //var test = SelectedRace.Teams;
            //var race = await _dataService.GetRaceAsync(SelectedRace.Id);
            //base.OnInitialize();
            //OnActivate();
            //IsBusy = true;

            //foreach (var team in race.Teams)
            //{
            //    Teams.Add(team);
            //}
            //IsBusy = false;
        }
        #endregion
    }    
}
