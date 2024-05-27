using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.EntityManagers
{
    public class ReviewManager : IReviewManager
    {
        private IReviewDataAccess _reviewDataAccess;

        public ReviewManager(IReviewDataAccess reviewDataAccess)
        {
            _reviewDataAccess = reviewDataAccess ?? throw new ArgumentNullException(nameof(_reviewDataAccess));
        }

        public void CreateReview(Review newReview)
        {
            _reviewDataAccess.CreateReview(newReview);
        }

        public Review GetReviewByID(int reviewID)
        {
            return _reviewDataAccess.GetReviewByID(reviewID);
        }

        public List<Review> GetAllReviews()
        {
            return _reviewDataAccess.GetAllReviews();
        }

        public void UpdateReview(Review review)
        {
            _reviewDataAccess.UpdateReview(review);
        }

        public void DeleteReviewByID(int reviewID)
        {
            _reviewDataAccess.DeleteReviewByID(reviewID);
        }
    }
}