using System;
using System.Collections.Generic;
using InfusionGames.CityScramble.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace InfusionGames.CityScramble.Controls
{
	public class CustomMap : Map
	{
		public List<CustomPin> CustomPins { get; private set; }

		public CustomMap()
		{
			CustomPins = new List<CustomPin>();
		}

		public void AddCustomPin(CustomPin pin)
		{
			CustomPins.Add(pin);
			if (Device.OS == TargetPlatform.iOS)
			{
				// In iOS the pins need to be added to the forms map control
				// in order to initiate a render callback
				Pins.Add(pin.FormsPin);
			}
			// In Android we are creating the pins 
			// based of the custom Pins collection in this class
		}

		public void Clear()
		{
			if (Device.OS == TargetPlatform.iOS)
			{
				Pins.Clear();
			}
			CustomPins.Clear();
		}
	}
}
