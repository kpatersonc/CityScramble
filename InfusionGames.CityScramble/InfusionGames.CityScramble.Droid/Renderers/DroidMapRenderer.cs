using System;
using System.ComponentModel;
using System.Linq;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using InfusionGames.CityScramble.Controls;
using InfusionGames.CityScramble.Droid.Renderers;
using InfusionGames.CityScramble.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(DroidMapRenderer))]
namespace InfusionGames.CityScramble.Droid.Renderers
{
	public class DroidMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
	{
		private GoogleMap _nativeMap;
		private CustomMap _formsMap;
		private bool _needsUpdate;

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				_nativeMap.InfoWindowClick -= OnInfoWindowClick;
			}

			if (e.NewElement != null)
			{
				var formsMap = (CustomMap)e.NewElement;
				_formsMap = formsMap;
				_formsMap.Pins.Clear();
				((MapView)Control).GetMapAsync(this);
			}
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			_nativeMap = googleMap;
			_nativeMap.InfoWindowClick += OnInfoWindowClick;
			_nativeMap.SetInfoWindowAdapter(this);
			UpdatePins();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName.Equals("VisibleRegion") && !_needsUpdate)
			{
				// Will update pins when 
				//UpdatePins();
			}
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);

			if (changed)
			{
				_needsUpdate = true;
				UpdatePins();
			}
		}

		private void UpdatePins()
		{
			if (_needsUpdate && _nativeMap != null)
			{
				_formsMap.Pins.Clear();

				foreach (var pin in _formsMap.CustomPins)
				{
					var marker = new MarkerOptions();
					marker.SetPosition(new LatLng(pin.FormsPin.Position.Latitude, pin.FormsPin.Position.Longitude));
					marker.SetTitle(pin.FormsPin.Label);
					marker.SetSnippet(pin.FormsPin.Address);
					var imageId = (int)typeof(Resource.Drawable).GetField(pin.ImageName).GetValue(null);
					marker.SetIcon(BitmapDescriptorFactory.FromResource(imageId));

					_nativeMap.AddMarker(marker);
				}
				_needsUpdate = true;
			}
		}

		private void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			var customPin = GetCustomPin(e.Marker);
			if (customPin == null)
			{
				throw new Exception("Custom pin not found");
			}

			customPin.OnClicked();
		}

		public Android.Views.View GetInfoContents(Marker marker)
		{
			// use default rendering of the info window content
			return null;
		}

		public Android.Views.View GetInfoWindow(Marker marker)
		{
			// use default window rendering
			return null;
		}

		private CustomPin GetCustomPin(Marker annotation)
		{
			var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);

			var pin = _formsMap.CustomPins.FirstOrDefault(p => p.FormsPin.Position == position);

			return pin;
		}

	}
}
