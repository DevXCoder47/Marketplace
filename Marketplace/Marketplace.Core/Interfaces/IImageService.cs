using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface IImageService
    {
        public Task<IEnumerable<Image>> GetImages(int skip, int take);
        public Task<Image> AddImage(Image image);
        public Task<Image> GetImageById(int id);
        public Task DeleteImage(int id);
    }
}
