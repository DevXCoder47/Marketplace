using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface IImageService
    {
        public Task<IEnumerable<Image>> GetImages(int skip, int take);
        public Task<Image> AddImage(Image image);
        public Task<Image> GetImageById(string id);
        public Task DeleteImage(string id);
    }
}
