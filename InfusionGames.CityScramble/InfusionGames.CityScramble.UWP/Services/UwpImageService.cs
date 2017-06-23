using InfusionGames.CityScramble.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace InfusionGames.CityScramble.UWP.Services
{
    public class UwpImageService : IImageService
    {
        public byte[] CompressImage(byte[] imageData)
        {
            return CompressImageAsync(imageData).Result;
        }

        public async Task<byte[]> CompressImageAsync(byte[] imageData)
        {
            return imageData;

            // below should work but is distorting images slightly...
            //IRandomAccessStream inStream = null;
            //IRandomAccessStream outStream = null;
            //try
            //{
            //    inStream = new MemoryStream(imageData).AsRandomAccessStream();
            //    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(inStream);

            //    uint height = decoder.PixelHeight / 2;
            //    uint width = decoder.PixelWidth / 2;

            //    // transform image data
            //    var transform = new BitmapTransform { ScaledHeight = height, ScaledWidth = width };
            //    var pixelData = await decoder.GetPixelDataAsync(
            //        BitmapPixelFormat.Rgba8,
            //        BitmapAlphaMode.Straight,
            //        transform,
            //        ExifOrientationMode.RespectExifOrientation,
            //        ColorManagementMode.DoNotColorManage);

            //    // setup encoder and output stream
            //    var propertySet = new BitmapPropertySet();
            //    var qualityValue = new BitmapTypedValue(1.0, Windows.Foundation.PropertyType.Single);
            //    propertySet.Add("ImageQuality", qualityValue);
            //    outStream = new InMemoryRandomAccessStream();
            //    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, outStream, propertySet);

            //    // populate encoder with resized data and write to stream
            //    encoder.SetPixelData(
            //        BitmapPixelFormat.Rgba8,
            //        BitmapAlphaMode.Straight,
            //        width, height,
            //        decoder.DpiX,
            //        decoder.DpiY,
            //        pixelData.DetachPixelData());
            //    await encoder.FlushAsync();

            //    // read bytes
            //    using (var dataReader = new DataReader(outStream.GetInputStreamAt(0)))
            //    {
            //        var result = new byte[outStream.Size];
            //        await dataReader.LoadAsync((uint)outStream.Size);
            //        dataReader.ReadBytes(result);
            //        return result;
            //    }
            //}
            //finally
            //{
            //    if (inStream != null)
            //        inStream.Dispose();

            //    if (outStream != null)
            //        outStream.Dispose();
            //}
        }
    }
}
