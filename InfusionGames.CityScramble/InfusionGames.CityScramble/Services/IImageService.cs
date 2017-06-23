using System.Threading.Tasks;

namespace InfusionGames.CityScramble.Services
{
    public interface IImageService
    {
        byte[] CompressImage(byte[] imageData);
        Task<byte[]> CompressImageAsync(byte[] imageData);
    }
}
