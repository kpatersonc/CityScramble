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
    public class JoinTeamViewModel : BaseScreen
    {

        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly ISettingsService _settingService;
        private string _teamCode;
        public string TeamCode {
            get {
                return _teamCode;
            }
            set {
                //_teamCode = value;
                SetField(ref _teamCode, value);
            }
        }
        public JoinTeamViewModel(
            INavigationService navigationService,
            IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            DisplayName = "Join Team";

            JoinTeam = new Command(OnJoinTeam);
        }

        public ICommand JoinTeam { get; protected set; }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        private async void OnJoinTeam() {
            IsBusy = true;

            await Task.Yield();
            var team = await _dataService.JoinTeamAsync(TeamCode);
            if (team != null) {
                _settingService.MyTeamId = team.Id;
                _settingService.MyTeamName = team.Name;
            }

            IsBusy = false;
        }
      

    }
}
