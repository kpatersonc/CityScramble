using MapKit;

namespace InfusionGames.CityScramble.iOS.Renderers
{
	public class CustomMKAnnotationView : MKAnnotationView
	{
		public readonly string Id;

		public CustomMKAnnotationView(IMKAnnotation annotation, string id)
		{
			Annotation = annotation;
			Id = id;
		}
	}
}