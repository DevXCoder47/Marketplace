using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetImages(int skip, int take);
        Task<Image> AddImage(Image image);
        Task<Image> GetImageById(string id);
        Task DeleteImage(string id);
    }
}
