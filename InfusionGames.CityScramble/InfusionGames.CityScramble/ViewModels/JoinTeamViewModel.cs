using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.ViewModels
{
    //no clue why we are inhereting BaseScreen here (as an example was taken RaceSelectionViewModel). figure it out.
    class JoinTeamViewModel : BaseScreen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public BindableCollection<Team> Teams { get; protected set; }

        public Team CurrentTeam { get; protected set; }
        public JoinTeamViewModel(
            INavigationService navigationService,
            IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            Teams = new BindableCollection<Team>();

            DisplayName = "Teams";

            RefreshCommand = new Command(OnRefresh);
            OnRefresh();
        }

        public ICommand RefreshCommand { get; protected set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            OnRefresh();
        }

        private async void OnRefresh()
        {
            IsBusy = true;

            await Task.Yield();

            // Clear the list or you'll get duplicates upon resuming
            Teams.Clear();

            // get the list of races
            IEnumerable<Team> teams = await _dataService.GetTeams();
            
            foreach (Team t in teams)
            {
                Teams.Add(t);
            }

            IsBusy = false;
        }
        
    }
}
