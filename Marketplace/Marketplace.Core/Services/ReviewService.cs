using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Services
{
    public class ReviewService : IReviewService
    {

        private readonly IRepository _repository;
        public ReviewService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Review> AddReview(Review review)
        {
            return await _repository.Add(review);
        }

        public async Task DeleteReview(string id)
        {
            await _repository.Delete<Review>(id);
        }

        public async Task<Review> GetReviewById(string id)
        {
            var review = await _repository.GetByIdAsync<Review>(id);

            if (review == null)
                throw new ArgumentException($"Review with id {id} not found");

            return review;
        }

        public async Task<IEnumerable<Review>> GetReviews(int skip, int take)
        {
            return await _repository.GetAll<Review>()
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync();
        }

        public async Task<Review> UpdateReview(string id, Review review)
        {
            return await _repository.Update(review, id);
        }
    }
}
