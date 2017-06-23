using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace InfusionGames.CityScramble.UWP.Renderers
{
    public sealed partial class CluePushpinOverlay : UserControl
    {
        #region Title
        public static readonly DependencyProperty TitleProperty =
    DependencyProperty.Register("Title", typeof(string), typeof(CluePushpinOverlay), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }  
        #endregion

        public event RoutedEventHandler Click
        {
            add { this.button.Click += value; }
            remove { this.button.Click -= value; }
        }

        public CluePushpinOverlay()
        {
            this.InitializeComponent();
        }        
    }
}
