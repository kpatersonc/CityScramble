using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using InfusionGames.CityScramble.Services;

namespace InfusionGames.CityScramble.Droid.Services
{
    public class DroidImageService : IImageService
    { 
        public byte[] CompressImage(byte[] imageData)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, originalImage.Width/2, originalImage.Height/2, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 40, ms);
                return ms.ToArray();
            }
        }

        public Task<byte[]> CompressImageAsync(byte[] imageData)
        {
            return Task.FromResult(CompressImage(imageData));
        }
    }
}