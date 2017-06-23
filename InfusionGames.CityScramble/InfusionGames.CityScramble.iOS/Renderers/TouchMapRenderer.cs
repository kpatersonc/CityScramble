using System;
using System.Linq;
using CoreGraphics;
using InfusionGames.CityScramble.Controls;
using InfusionGames.CityScramble.iOS.Renderers;
using InfusionGames.CityScramble.Models;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(TouchMapRenderer))]
namespace InfusionGames.CityScramble.iOS.Renderers
{
	public class TouchMapRenderer : MapRenderer
	{
		private CustomMap _formsMap;

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = null;
				nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
			}

			if (e.NewElement != null)
			{
				_formsMap = (CustomMap)e.NewElement;
				var nativeMap = Control as MKMapView;

				nativeMap.GetViewForAnnotation = GetViewForAnnotation;
				nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
				nativeMap.ShowsUserLocation = true;
			}
		}

		private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;

			if (annotation is MKUserLocation)
			{
				return null;
			}

			var anno = annotation as MKPointAnnotation;

			if (anno == null)
			{
				return null;
			}

			var customPin = GetCustomPin(anno);
			if (customPin == null)
			{
				throw new Exception("Custom pin not found");
			}

			annotationView = mapView.DequeueReusableAnnotation(customPin.Id);
			if (annotationView == null)
			{
				annotationView = new CustomMKAnnotationView(annotation, customPin.Id);
				annotationView.Image = UIImage.FromFile(customPin.ImageNameWithExt);
				annotationView.CalloutOffset = new CGPoint(0, 0);
				annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
			}
			annotationView.CanShowCallout = true;

			return annotationView;
		}

		private CustomPin GetCustomPin(MKPointAnnotation annotation)
		{
			var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);

			var pin = _formsMap.CustomPins.FirstOrDefault(p => p.FormsPin.Position == position);

			return pin;
		}
			
		private void OnCalloutAccessoryControlTapped (object sender, MKMapViewAccessoryTappedEventArgs e)
		{
			var customView = e.View as CustomMKAnnotationView;
			var customPin = _formsMap.CustomPins.FirstOrDefault(p => p.Id == customView.Id);

			if (customPin != null)
			{
				customPin.OnClicked();
			}

		}
	}
}
