using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
