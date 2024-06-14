using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.RecommendationAlgorithm
{
    public class RecommendationEngine
    {
        private IBookManager _bookManager;
        private IGenreManager _genreManager;
        private IUserManager _userManager;
        private SimilarityCalculator _similarityCalculator;

        public RecommendationEngine(IBookManager bookManager, IGenreManager genreManager, IUserManager userManager)
        {
            _bookManager = bookManager ?? throw new ArgumentNullException(nameof(_bookManager));
            _genreManager = genreManager ?? throw new ArgumentNullException(nameof(_genreManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _similarityCalculator = new SimilarityCalculator();
        }

        public async Task<List<Book>> GetRecommendationsForUserAsync(int userID, int topN = 10)
        {
            var allBooks = await _bookManager.GetAllBooksWithDetailsAsync();

            var userRatings = await _userManager.GetReviewsByUserAsync(userID);
            var userFavorites = await _userManager.GetFavoritesByUserAsync(userID);

            var userHighlyRatedBookIDs = userRatings.Where(review => review.BookRating >= 4).Select(review => review.SourceBook.ID).ToList();
            var userPreferredBooks = allBooks.Where(book => userHighlyRatedBookIDs.Contains(book.ID)).ToList();

            var maxPageCount = await _bookManager.GetMaxPageCountAsync();

            var bookFeatureVectors = allBooks.ToDictionary(book => book.ID, book => new FeatureVector(book, maxPageCount));
            var userPreferredFeatureVectors = userPreferredBooks.ToDictionary(book => book.ID, book => new FeatureVector(book, maxPageCount));

            var bookSimiliarityScores = new Dictionary<int, double>();

            foreach (var book in allBooks)
            {
                if (userPreferredFeatureVectors.ContainsKey(book.ID))
                {
                    continue;
                }

                double totalSimilarity = 0.0;
                foreach (var preferredBook in userPreferredBooks)
                {
                    double weight = 1.0;
                    var rating = userRatings.FirstOrDefault(rating => rating.SourceBook.ID == preferredBook.ID)?.BookRating ?? 0;
                    if (userFavorites.Contains(preferredBook))
                    {
                        weight = 2.0;
                    }

                    totalSimilarity += weight * _similarityCalculator.CalculateCosineSimilarity(
                        bookFeatureVectors[book.ID],
                        userPreferredFeatureVectors[preferredBook.ID]
                    );
                }
                bookSimiliarityScores[book.ID] = totalSimilarity / userPreferredBooks.Count;
            }

            return bookSimiliarityScores.OrderByDescending(keyValuePair => keyValuePair.Value)
                                        .Take(topN)
                                        .Select(keyValuePair => allBooks.First(book => book.ID == keyValuePair.Key))
                                        .ToList();
        }
    }
}
