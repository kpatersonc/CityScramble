using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.ViewModels
{
    /// <summary>
    /// Race Selection ViewModel represents the main screen at the app that let's you 
    /// select a Race
    /// </summary>
    public class RaceSelectionViewModel : BaseScreen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        private Race _currentRace;

        public RaceSelectionViewModel(
            INavigationService navigationService,
            IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            DisplayName = "Home";

            Races = new BindableCollection<Group<Race>>();

            RefreshCommand = new Command(OnRefresh);
        }

        public BindableCollection<Group<Race>> Races { get; protected set; }

        public ICommand RefreshCommand { get; protected set; }

        public Race CurrentRace
        {
            get { return _currentRace; }
            set { SetField(ref _currentRace, value); }
        }

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
            Races.Clear();

            // get the list of races
            IEnumerable<Race> races = await _dataService.GetRacesAsync();

            var previous = new Group<Race>() { Name = "Previous Races" };
            var current = new Group<Race>() { Name = "Current Races" };
            var future = new Group<Race>() { Name = "Upcoming Races" };

            var now = DateTimeOffset.Now;

            foreach (var race in races)
            {
                if (race.StartDate > now)
                {
                    future.Add(race);
                }
                else if (race.EndDate < now)
                    previous.Add(race);
                else
                    current.Add(race);
            }

            // sort items
            current.Sort(i => i.StartDate ?? DateTimeOffset.MinValue);
            future.Sort(i => i.StartDate ?? DateTimeOffset.MinValue);
            previous.SortDescending(i => i.EndDate ?? DateTimeOffset.MaxValue);

            Races
                .AddGroupIfNotEmpty(current)
                .AddGroupIfNotEmpty(future)
                .AddGroupIfNotEmpty(previous);

            IsBusy = false;
        }
        
        public void ViewRace(object race)
        {
            Settings.Current.RaceId = CurrentRace.Id;

            // navigate
            _navigationService
                .For<TabbedViewModel>()
                .WithParam(x => x.SelectedRace, CurrentRace)
                .Navigate();
        }
    }
}
