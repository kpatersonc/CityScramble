using Caliburn.Micro;
using Xamarin.Forms.Platform.UWP;

namespace InfusionGames.CityScramble.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();


            LoadApplication(IoC.Get<InfusionGames.CityScramble.App>());
        }

        
    }
}
