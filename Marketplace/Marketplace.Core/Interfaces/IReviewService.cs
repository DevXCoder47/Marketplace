using Marketplace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetReviews(int skip, int take);
        Task<Review> GetReviewById(string id);
        Task<Review> AddReview(Review review);
        Task<Review> UpdateReview(string id, Review review);
        Task DeleteReview(string id);
    }
}
