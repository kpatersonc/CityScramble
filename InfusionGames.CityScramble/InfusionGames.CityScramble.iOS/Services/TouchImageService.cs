using System;
using System.Drawing;
using System.Threading.Tasks;
using CoreGraphics;
using InfusionGames.CityScramble.Services;
using UIKit;

namespace InfusionGames.CityScramble.iOS.Services
{
    public class TouchImageService : IImageService
    {
        public byte [] CompressImage (byte [] imageData)
        {
			var scaleFactor = 2;

			UIImage originalImage = ImageFromByteArray(imageData);

			var width = (int)originalImage.Size.Width / scaleFactor;
			var height = (int)originalImage.Size.Height / scaleFactor;

			UIGraphics.BeginImageContextWithOptions(new CGSize(width, height), true, 0.5f);

			originalImage.Draw(new CGRect(0, 0, width, height));
            
			var resized = UIGraphics.GetImageFromCurrentImageContext();

            return resized.AsJPEG(0.5F).ToArray();
        }

        public Task<byte[]> CompressImageAsync(byte[] imageData)
        {
            return Task.FromResult(CompressImage(imageData));
        }

        public static UIImage ImageFromByteArray(byte[] data)
		{
			if (data == null)
			{
				return null;
			}

			UIImage image;
			try
			{
				image = new UIImage(Foundation.NSData.FromArray(data));
			}
			catch (Exception e)
			{
				Console.WriteLine("Image load failed: " + e.Message);
				return null;
			}
			return image;
		}
    }
}