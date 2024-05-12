using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class ReviewManager : IReviewManager
    {
        private IReviewDataAccess _reviewDataAccess;

        public ReviewManager(IReviewDataAccess reviewDataAccess)
        {
            _reviewDataAccess = reviewDataAccess;
        }

        public void CreateReview(Review newReview)
        {
            try { _reviewDataAccess.CreateReview(newReview); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public Review GetReviewByID(int reviewID)
        {
            try { return _reviewDataAccess.GetReviewByID(reviewID); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Review> GetAllReviews()
        {
            try { return _reviewDataAccess.GetAllReviews(); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void UpdateReview(Review review)
        {
            try { _reviewDataAccess.UpdateReview(review); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteReviewByID(int reviewID)
        {
            try { _reviewDataAccess.DeleteReviewByID(reviewID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}