using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewDataAccess _reviewDataAccess;

        public ReviewManager(IReviewDataAccess reviewDataAccess)
        {
            _reviewDataAccess = reviewDataAccess ?? throw new ArgumentNullException(nameof(_reviewDataAccess));
        }

        public async Task CreateReviewAsync(Review newReview)
        {
            await _reviewDataAccess.CreateReviewAsync(newReview);
        }

        public async Task<Review> GetReviewByIDAsync(int reviewID)
        {
            return await _reviewDataAccess.GetReviewByIDAsync(reviewID);
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _reviewDataAccess.GetAllReviewsAsync();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewDataAccess.UpdateReviewAsync(review);
        }

        public async Task DeleteReviewByIDAsync(int reviewID)
        {
            await _reviewDataAccess.DeleteReviewByIDAsync(reviewID);
        }
    }
}