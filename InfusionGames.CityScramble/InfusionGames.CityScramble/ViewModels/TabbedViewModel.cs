using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;

namespace InfusionGames.CityScramble.ViewModels
{
    public class TabbedViewModel : Conductor<BaseScreen>.Collection.OneActive
    {
        private readonly IList<IRaceTab> _tabs;
        private readonly ILocationService _locationService;
        private readonly IPushNotificationService _pushService;

        public TabbedViewModel(
            IEnumerable<IRaceTab> tabs,
            ILocationService locationService,
            IPushNotificationService pushService)
        {
            _tabs = tabs.OrderBy(i => i.Priority).ToList();
            _locationService = locationService;
            _pushService = pushService;
        }

        private Race _raceDetails;

        public Race SelectedRace
        {
            get { return _raceDetails; }
            set
            {
                _raceDetails = value;

                Items.Clear();

                foreach (var tab in _tabs)
                {
                    if (tab.IsSupported(_raceDetails))
                    {
                        tab.SelectedRace = _raceDetails;
                        Items.Add((BaseScreen)tab);
                    }
                }

                DisplayName = _raceDetails.Name;

                ActivateItem(Items[0]);
            }
        }

        protected async override void OnActivate()
        {
            base.OnActivate();

            //await _locationService.StartSendingLocation(SelectedRace);
            //await _pushService.RegisterForPush();
        }
    }

    /// <summary>
    /// Represents a Tab on the Race Details screen
    /// </summary>
    public interface IRaceTab
    {
		/// <summary>
		/// Gets tab title.
		/// </summary>
		/// <value>The title.</value>
		string Title { get; }

		/// <summary>
		/// Gets tab icon.
		/// </summary>
		/// <value>The icon.</value>
		string Icon { get; }

        /// <summary>
        /// The display order for the tab
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Get or Set the Race for the tab
        /// </summary>
        Race SelectedRace { get; set; }

        /// <summary>
        /// Check if this Tab should be displayed as part of this Race
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
        bool IsSupported(Race race);
    }
}
