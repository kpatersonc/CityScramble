using InfusionGames.CityScramble.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace InfusionGames.CityScramble.Views
{
    public partial class MapView : ContentView
    {
        public MapView()
        {
            InitializeComponent();
        }

        public CustomMap Map
        {
            get { return this.myMap; }
        }
    }
}
