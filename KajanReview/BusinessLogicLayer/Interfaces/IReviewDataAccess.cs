using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IReviewDataAccess
    {
        public void CreateReview(Review newReview);
        public Review GetReviewByID(int reviewID);
        public List<Review> GetAllReviews();
        public void UpdateReview(Review review);
        public void DeleteReviewByID(int reviewID);
    }
}
