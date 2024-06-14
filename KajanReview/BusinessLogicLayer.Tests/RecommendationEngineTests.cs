using NUnit.Framework;
using Moq;
using BusinessLogicLayer.RecommendationAlgorithm;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace YourProjectName.Tests
{
    [TestFixture]
    public class RecommendationEngineTests
    {
        [Test]
        public async Task GetRecommendationsForUserAsync_ValidUser_ReturnsTopNRecommendations()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var userManagerMock = new Mock<IUserManager>();
            var genreManagerMock = new Mock<IGenreManager>();

            bookManagerMock.Setup(m => m.GetAllBooksWithDetailsAsync()).ReturnsAsync(new List<Book>
            {
                new Book { ID = 1, PageCount = 100 },
                new Book { ID = 2, PageCount = 200 }
            });

            userManagerMock.Setup(m => m.GetReviewsByUserAsync(It.IsAny<int>())).ReturnsAsync(new List<Review>
            {
                new Review { SourceBook = new Book { ID = 1 }, BookRating = 5 }
            });

            userManagerMock.Setup(m => m.GetFavoritesByUserAsync(It.IsAny<int>())).ReturnsAsync(new List<Book>
            {
                new Book { ID = 1 }
            });

            var engine = new RecommendationEngine(bookManagerMock.Object, genreManagerMock.Object, userManagerMock.Object);
            var recommendations = await engine.GetRecommendationsForUserAsync(1, 1);

            Assert.Equals(1, recommendations.Count);
            Assert.Equals(2, recommendations.First().ID);
        }

        // Add more tests here as needed
    }
}