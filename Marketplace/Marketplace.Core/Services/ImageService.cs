using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IRepository _repository;
        public ImageService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Image>> GetImages(int skip, int take)
        {
            return await _repository.GetAll<Image>().
                Include(i => i.Product).
                AsNoTracking().
                Skip(skip).
                Take(take).
                ToArrayAsync();
        }
        public Task<Image> AddImage(Image image)
        {
            if (string.IsNullOrEmpty(image.FilePath))
                throw new ArgumentException("Invalid image build");  // TODO own types of exceptions
            return _repository.Add(image);
        }
        public async Task<Image> GetImageById(int id)
        {
            var image = await _repository.GetByIdAsync<Image>(id);
            if (image == null)
                throw new ArgumentException("Image not found");    // TODO own types of exceptions
            return image;
        }
        public async Task DeleteImage(int id)
        {
            await _repository.Delete<Image>(id);
        }
    }
}
