using Caliburn.Micro;
using InfusionGames.CityScramble.Models;

namespace InfusionGames.CityScramble.ViewModels
{
    public class TeamClueViewModel : BaseScreen
    {

        public TeamClue Clue { get; set; }
        private bool _isRaceActive;

        public TeamClueViewModel()
        {

            DisplayName = "Clue";
        }

        public bool CanSubmit
        {   
            get { return _isRaceActive && Clue?.Status != ClueStatus.Complete; }
            set { _isRaceActive = value; }
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
