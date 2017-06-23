using System;
using Xamarin.Forms.Maps;

namespace InfusionGames.CityScramble.Models
{
	public class CustomPin
	{
		public Pin FormsPin { get; set; }

		public string ImageName { get; set;}

		public string ImageNameWithExt => ImageName + ".png";

	    public EventHandler Clicked;

		public string Id => FormsPin.GetHashCode().ToString();

	    public CustomPin(Pin pin, string imageName)
		{
			FormsPin = pin;

			ImageName = imageName;
			
		}

		public void OnClicked()
		{
			var handler = Clicked;

		    handler?.Invoke(this, new EventArgs());
		}

	}
}
