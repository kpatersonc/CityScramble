using Caliburn.Micro;
using InfusionGames.CityScramble.Models;

namespace InfusionGames.CityScramble.ViewModels
{
    public class TeamClueViewModel : PropertyChangedBase
    {

        public TeamClue Clue;
        private readonly bool _isRaceActive;

        public TeamClueViewModel(TeamClue clue, bool isRaceActive)
        {
            Clue = clue;
            _isRaceActive = isRaceActive;
        }

        public bool CanSubmit
        {
            get { return _isRaceActive && Clue?.Status != ClueStatus.Complete; }
        }

        public string DisplayTitle
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Clue?.Name))
                {
                    return Clue.Name;
                }
                return Clue?.Description;
            }
        }

        public string DisplayDescription
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Clue?.Description))
                {
                    return Clue.Description;
                }
                return string.Empty;
            }
        }

        public bool HasResponse => Clue.HasResponse();

        public string PointsString => Clue.PointsString;

        public ClueKind Kind => Clue.Kind;
    }
}
