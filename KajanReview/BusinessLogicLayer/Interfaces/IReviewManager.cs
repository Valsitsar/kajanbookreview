using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IReviewManager
    {
        public Task CreateReviewAsync(Review newReview);
        public Task<Review> GetReviewByIDAsync(int reviewID);
        public Task<List<Review>> GetAllReviewsAsync();
        public Task UpdateReviewAsync(Review review);
        public Task DeleteReviewByIDAsync(int reviewID);
    }
}
