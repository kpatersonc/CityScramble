using System;
using System.ComponentModel;
using System.Linq;
using InfusionGames.CityScramble.Controls;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.UWP.Renderers;
using InfusionGames.CityScramble.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.Maps.UWP;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;


[assembly: ExportRenderer(typeof(CustomMap), typeof(UWPMapRender))]
namespace InfusionGames.CityScramble.UWP.Renderers
{
	public class UWPMapRender : MapRenderer
	{
        CustomMap _formsMap;
        MapControl _nativeMap;
        CluePushpinOverlay _overlay;
        bool _overlayShown;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _formsMap = null;

                _nativeMap.Children.Clear();
                _nativeMap.MapElementClick -= OnMapElementClick;
                _nativeMap = null;
            }

            if (e.NewElement != null)
            {
                _formsMap = (CustomMap)e.NewElement;
                _formsMap.IsShowingUser = true;

                _nativeMap = Control as MapControl;
                _nativeMap.MapElementClick += OnMapElementClick;
                _nativeMap.ZoomInteractionMode = MapInteractionMode.GestureOnly;
                

                UpdatePins();   
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "VisibleRegion")
            {
                UpdatePins();
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;

            if (mapIcon != null)
            {
                // TODO:
                // if it's for the same pin, hide it
                // if it's for a different pin, switch it out

                if (!_overlayShown)
                {
                    var customPin = GetCustomPin(mapIcon.Location);

                    _overlay = new CluePushpinOverlay()
                    {
                        Title = customPin.FormsPin.Label
                    };
                    _overlay.Click += (o, e) => customPin.OnClicked();

                    _nativeMap.Children.Add(_overlay);
                    MapControl.SetLocation(_overlay, mapIcon.Location);
                    MapControl.SetNormalizedAnchorPoint(_overlay, new Windows.Foundation.Point(0.5, 1.0));
                    _overlayShown = true;
                }
                else
                {
                    _nativeMap.Children.Remove(_overlay);
                    _overlayShown = false;
                }
            }
        }

        private void UpdatePins()
        {
            _nativeMap.Children.Clear();
            _nativeMap.MapElements.Clear();

            foreach (var customPin in _formsMap.CustomPins)
            {
                MapIcon icon = CreateMapIcon(customPin);

                _nativeMap.MapElements.Add(icon);
            }
        }

        private MapIcon CreateMapIcon(CustomPin pin)
        {
            Position customPinPosition = pin.FormsPin.Position;
            var position = new BasicGeoposition
            {
                Latitude = customPinPosition.Latitude,
                Longitude = customPinPosition.Longitude
            };
            var point = new Geopoint(position);

            var mapIcon = new MapIcon
            {
                Location = point,
                Image = RandomAccessStreamReference.CreateFromUri(new Uri($"ms-appx:///Assets/{pin.ImageNameWithExt}", UriKind.Absolute)),
                CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible,
                NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0)
            };

            return mapIcon;
        }

        private CustomPin GetCustomPin(Geopoint point)
        {
            return _formsMap.CustomPins.FirstOrDefault(
                x => 
                    x.FormsPin.Position.Latitude == point.Position.Latitude &&
                    x.FormsPin.Position.Longitude == point.Position.Longitude);
        }
    }
}
